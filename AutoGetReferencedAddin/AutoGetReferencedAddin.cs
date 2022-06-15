/*
The MIT License (MIT)

Copyright(c) 2022, Jim Sculley

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using EdmLib;

using SldWorks;
using SolidWorksTools;
using SWPublished;

namespace org.duckdns.buttercup.autogetreferenced
{
    [Guid("D6A460EB-A364-48AD-B7DF-880754DF5477"), ComVisible(true)]
    [SwAddin(
        Description = "Automatically get referenced versions of files",
        Title = "AutoGet Referenced File Versions",
        LoadAtStartup = true
        )]
    public sealed class AutoGetReferencedAddin : ISwAddin
    {
        #region Private Variables
        /// <summary>
        /// The SOLIDWORKS application instance
        /// </summary>
        private ISldWorks swApp = null;
        /// <summary>
        /// The cookie supplied to the add-in by SOLIDWORKS
        /// </summary>
        private int addinID = 0;
        /// <summary>
        /// The EPDM vaults where this add-in is active
        /// </summary>
        private Dictionary<String, IEdmVault21> connectedVaults = new Dictionary<string, IEdmVault21>();
        #endregion

        #region Event Handler Variables
        SldWorks.SldWorks SwEventPtr = null;
        #endregion

        #region Boilerplate SolidWorks Registration Code
        [ComRegisterFunctionAttribute]
        public static void RegisterFunction(Type t)
        {
            #region Get Custom Attribute: SwAddinAttribute
            SwAddinAttribute SWattr = null;
            Type type = typeof(AutoGetReferencedAddin);

            foreach (System.Attribute attr in type.GetCustomAttributes(false))
            {
                if (attr is SwAddinAttribute)
                {
                    SWattr = attr as SwAddinAttribute;
                    break;
                }
            }

            #endregion

            try
            {
                Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                string keyname = "SOFTWARE\\SolidWorks\\Addins\\{" + t.GUID.ToString() + "}";
                Microsoft.Win32.RegistryKey addinkey = hklm.CreateSubKey(keyname);
                addinkey.SetValue(null, 0);

                addinkey.SetValue("Description", SWattr.Description);
                addinkey.SetValue("Title", SWattr.Title);

                keyname = "Software\\SolidWorks\\AddInsStartup\\{" + t.GUID.ToString() + "}";
                addinkey = hkcu.CreateSubKey(keyname);
                addinkey.SetValue(null, Convert.ToInt32(SWattr.LoadAtStartup), Microsoft.Win32.RegistryValueKind.DWord);
            }
            catch (System.NullReferenceException nl)
            {
                Console.WriteLine("There was a problem registering this dll: SWattr is null. \n\"" + nl.Message + "\"");
                MessageBox.Show("There was a problem registering this dll: SWattr is null.\n\"" + nl.Message + "\"");
            }

            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);

                MessageBox.Show("There was a problem registering the function: \n\"" + e.Message + "\"");
            }
        }

        [ComUnregisterFunctionAttribute]
        public static void UnregisterFunction(Type t)
        {
            try
            {
                Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                string keyname = "SOFTWARE\\SolidWorks\\Addins\\{" + t.GUID.ToString() + "}";
                hklm.DeleteSubKey(keyname);

                keyname = "Software\\SolidWorks\\AddInsStartup\\{" + t.GUID.ToString() + "}";
                hkcu.DeleteSubKey(keyname);
            }
            catch (System.NullReferenceException nl)
            {
                Console.WriteLine("There was a problem unregistering this dll: " + nl.Message);
                System.Windows.Forms.MessageBox.Show("There was a problem unregistering this dll: \n\"" + nl.Message + "\"");
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show("There was a problem unregistering this dll: \n\"" + e.Message + "\"");
            }
        }

        #endregion

        #region Boilerplate SOLIDWORKS add-in code
        bool ISwAddin.ConnectToSW(object ThisSW, int cookie)
        {
            swApp = (ISldWorks)ThisSW;
            addinID = cookie;

            //Setup callbacks
            swApp.SetAddinCallbackInfo(0, this, addinID);


            #region Setup the Event Handlers
            SwEventPtr = (SldWorks.SldWorks)swApp;
            AttachEventHandlers();
            #endregion

            return true;
        }


        private int SwEventPtr_DestroyNotify()
        {
            DetachEventHandlers();            
            return 0;
        }

        bool ISwAddin.DisconnectFromSW()
        {
            DetachEventHandlers();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(swApp);
            swApp = null;
            //The addin _must_ call GC.Collect() here in order to retrieve all managed code pointers 
            GC.Collect();
            GC.WaitForPendingFinalizers();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return true;
        }
        #endregion

        #region Event Methods
        public bool AttachEventHandlers()
        {
            try
            {
                SwEventPtr.FileOpenPreNotify += SwEventPtr_FileOpenPreNotify;
                SwEventPtr.DestroyNotify += SwEventPtr_DestroyNotify;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool DetachEventHandlers()
        {
            try
            {
                SwEventPtr.FileOpenPreNotify -= SwEventPtr_FileOpenPreNotify;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// FileOpenPreNotify event handling method.  Prior to the file opening, retrieve the as-built
        /// referenced versions.  If the requested versions cannot be retrieved, notfy the user.
        /// </summary>
        /// <param name="pathToFileOpening">the path of the file bweing opened</param>
        /// <returns></returns>
        private int SwEventPtr_FileOpenPreNotify(string pathToFileOpening)
        {
            maybeShowFirstRunDialog();
            maybeConnectToVaults();
            if (connectedVaults.Count == 0) { return 0; } //Just return if no vaults are connected
            //Get the name of the vault where the file is located
            string[] directories = pathToFileOpening.Split(Path.DirectorySeparatorChar);
            string vaultName = directories[1];
            if (!connectedVaults.ContainsKey(vaultName)) { return 0; } //We're not connected to this vault
            IEdmVault21 vault = connectedVaults[vaultName];
            IEdmFile17 fileOpening = vault.GetFileFromPath(pathToFileOpening, out IEdmFolder5 vaultFolder) as IEdmFile17;
            IEdmBatchGet batchGet = vault.CreateUtility(EdmUtility.EdmUtil_BatchGet);
            batchGet.AddSelectionEx((EdmVault5)vault, fileOpening.ID, vaultFolder.ID, 0);
            EdmGetCmdFlags options =
                EdmGetCmdFlags.Egcf_AsBuilt |
                EdmGetCmdFlags.Egcf_SkipOpenFileChecks;
            batchGet.CreateTree(0, (int)options);
            if (batchGet.FileCount <= 1) { return 0; } //No referenced files, so just return
            if (Properties.Settings.Default.ShowGetDialog)
            {
                batchGet.ShowDlg(0);
            }
            batchGet.GetFiles(0, null);
            IEdmSelectionList6 filesMarkedToGet = batchGet.GetFileList((int)EdmGetFileListFlag.Egflf_GetRetrieved) as IEdmSelectionList6;
            List<VersionConflictInfo> versionConflicts = getVersionConflicts(vault, filesMarkedToGet);
            if (versionConflicts.Count == 0) {  return 0; } //No conflicts, just return
            DialogResult dr = new ReferenceVersionConflictForm(versionConflicts, Path.GetFileName(pathToFileOpening), swApp).ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return 1; //User pressed cancel, so canel the file opening operation
            }
            return 0;
        }
        #endregion
        /// <summary>
        /// Check to see if this is the first time the add-in has been executed, and if so, show the
        /// <see cref="FirstRun"/> dialog
        /// </summary>
        private void maybeShowFirstRunDialog()
        {
            if (Properties.Settings.Default.KnownVaults == null || Properties.Settings.Default.KnownVaults.Count == 0)
            {
                new FirstRun().ShowDialog();
            }
        }
        /// <summary>
        /// Check to see if any vaults are connected and if not, connect to them
        /// </summary>
        private void maybeConnectToVaults()
        {
            if (connectedVaults.Count == 0)
            {
                connectToVaults();
            }
        }
        /// <summary>
        /// Connect to all vaults in the application settings file
        /// </summary>
        private void connectToVaults()
        {
            if (Properties.Settings.Default.KnownVaults == null)
            {
                return;
            }
            foreach (string s in Properties.Settings.Default.KnownVaults)
            {
                try
                {
                    IEdmVault21 nextVault = new EdmVault5() as IEdmVault21;
                    nextVault.LoginAuto(s, 0);
                    connectedVaults.Add(nextVault.Name, nextVault);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to login to vault: " + s + "\n\nError message was: \n\n" + e.Message);
                }
            }
        }

        /// <summary>
        /// Analze a list of files that have been marked for retrieval as part of a 'get' operation and
        /// create a list of <see cref="VersionConflictInfo"/> objects for the files that cannot be retrieved
        /// </summary>
        /// <param name="vault">the vault from which the files are to be retrieved</param>
        /// <param name="filesMarkedToGet">the list of files marked to 'get'</param>
        /// <returns>a list of <see cref="VersionConflictInfo"/> objects</returns>
        private List<VersionConflictInfo> getVersionConflicts(IEdmVault21 vault, IEdmSelectionList6 filesMarkedToGet)
        {
            List<VersionConflictInfo> versionConflicts = new List<VersionConflictInfo>();
            IEdmPos5 pos = filesMarkedToGet.GetHeadPosition();
            while (!pos.IsNull)
            {
                filesMarkedToGet.GetNext2(pos, out EdmSelectionObject selection);
                IEdmFile17 retrievedFile = vault.GetObject(EdmObjectType.EdmObject_File, selection.mlID) as IEdmFile17;
                int localVersion = retrievedFile.GetLocalVersionNo2(selection.mbsPath, out bool obsolete);
                if (localVersion != selection.mlGetVersion)
                {
                    VersionConflictInfo nextConflict = new VersionConflictInfo();
                    nextConflict.Filename = retrievedFile.Name;
                    nextConflict.RequestedVersion = selection.mlGetVersion;
                    nextConflict.FoundVersion = localVersion;
                    nextConflict.FullPath = selection.mbsPath;
                    versionConflicts.Add(nextConflict);
                }
            }
            return versionConflicts;
        }
    }
}
