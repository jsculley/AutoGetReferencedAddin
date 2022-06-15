using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EdmLib;

namespace org.duckdns.buttercup.autogetreferenced
{
    internal sealed partial class VaultSetupForm : Form
    {
        public VaultSetupForm()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog fbDialog = new FolderBrowserDialog())
            {
                DialogResult dr = fbDialog.ShowDialog();
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                IEdmVault5 vault = new EdmVault5();
                try
                {
                    string vaultName = Path.GetFileName(fbDialog.SelectedPath);
                    if (vaultListBox.Items.Contains(vaultName))
                    {
                        return;
                    }
                    vault.LoginAuto(vaultName, 0);
                    vaultListBox.Items.Add(vaultName);
                }
                catch (Exception)
                {
                    MessageBox.Show("The selected location does not appear to be an EPDM vault", "Invalid Vault Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selItems = vaultListBox.SelectedItems;
            for(int i = selItems.Count - 1; i >= 0; i--)
            {
                vaultListBox.Items.Remove(selItems[i]);
            }
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.KnownVaults == null)
            {
                Properties.Settings.Default.KnownVaults = new System.Collections.Specialized.StringCollection();
            }
            Properties.Settings.Default.KnownVaults.Clear();
            Properties.Settings.Default.KnownVaults.AddRange(vaultListBox.Items.OfType<string>().ToArray());
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
