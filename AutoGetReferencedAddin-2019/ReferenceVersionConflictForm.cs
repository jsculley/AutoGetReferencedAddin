using SldWorks = SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace org.duckdns.buttercup.autogetreferenced
{
    internal sealed partial class ReferenceVersionConflictForm : Form
    {
        private SldWorks.ISldWorks swApp;
        private List<VersionConflictInfo> conflicts;
        private string openingFile;
        public ReferenceVersionConflictForm(List<VersionConflictInfo> conflicts, string openingFile, SldWorks.ISldWorks swApp)
        {
            this.swApp = swApp;
            this.conflicts = conflicts;
            this.openingFile = openingFile;
            InitializeComponent();
            this.versionConflictInfoBindingSource.DataSource = new BindingList<VersionConflictInfo>(conflicts);
            this.Text = this.Text + "-" + openingFile;
        }
        public ReferenceVersionConflictForm() : this(new List<VersionConflictInfo>(), "", null)
        {
        }

        private void conflictDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conflictDataGridView.Columns[e.ColumnIndex].Name == "Details")
            {
                string filename = conflictDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                string path = conflicts.Find(x => x.Filename.Equals(filename, StringComparison.OrdinalIgnoreCase)).FullPath;
                List<string> refDocPaths = getOpenReferencingDocumentPaths(path);
                new ReferencingFilesDialog(filename, refDocPaths).ShowDialog();
            }
        }

        private List<string> getOpenReferencingDocumentPaths(string path)
        {
            List<string> refDocPaths = new List<string>();
            if (isOpenInSolidWorks(path))
            {
                object[] openDocObjArray = swApp.GetDocuments() as object[];
                SldWorks.ModelDoc2[] openDocs = Array.ConvertAll(openDocObjArray, item => (SldWorks.ModelDoc2)item);
                foreach (SldWorks.ModelDoc2 mDoc in openDocs)
                {
                    if (mDoc.GetPathName().Equals(openingFile, StringComparison.OrdinalIgnoreCase)) { continue; }
                    if (mDoc.GetPathName().Equals(path, StringComparison.OrdinalIgnoreCase))
                    {
                        refDocPaths.Add(path);
                        continue;
                    }
                    string[] refData = swApp.GetDocumentDependencies2(mDoc.GetPathName(), true, false, true) as string[];
                    if (refData != null)
                    {
                        List<string> paths = new List<string>();
                        //Starting from the second element, get every third element thereafter
                        for (int i = 0; i < refData.Length; i++)
                        {
                            if (i % 3 == 0)
                            {
                                paths.Add(refData[i + 1]);
                            }
                        }
                        if (paths.Contains(path))
                        {
                            refDocPaths.Add(mDoc.GetPathName());
                        }
                    }
                }
            }
            return refDocPaths;
        }

        private bool isOpenInSolidWorks(string path)
        {
            object[] openDocObjArray = swApp.GetDocuments() as object[];
            SldWorks.ModelDoc2[] openDocs = Array.ConvertAll(openDocObjArray, item => (SldWorks.ModelDoc2)item);
            return Array.Find(openDocs, e => e.GetPathName().Equals(path, StringComparison.OrdinalIgnoreCase)) != null;
        }

        private void conflictDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            conflictDataGridView.Rows[0].Selected = false;
        }
    }
}
