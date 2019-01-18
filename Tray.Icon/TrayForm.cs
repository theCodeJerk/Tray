using System;
using System.Windows.Forms;
using theCodeJerk.Tray.Config;
using System.Diagnostics;

namespace theCodeJerk.Tray.Icon
{
    public partial class TrayForm : Form
    {
        /// <summary>
        ///
        /// </summary>
        public TrayForm()
        {
            InitializeComponent();
            notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UpdateServiceMenus);
            UpdateServiceMenus(this, new EventArgs());
            LoadConfigMenus(this, new EventArgs());
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void startKrakabotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serviceController1.Start();
            UpdateServiceMenus(sender, e);
        }

        private void stopKrakabotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serviceController1.Stop();
            UpdateServiceMenus(sender, e);
        }

        private void configureKrakabotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: run the gui application
            //Program.mainForm = new MainForm();
            //Program.mainForm.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UpdateServiceMenus(sender, e);
            configureKrakabotToolStripMenuItem_Click(sender, e);
        }

        private void UpdateServiceMenus(object sender, EventArgs e)
        {
            //startKrakabotToolStripMenuItem.Enabled = !KrakabotService.KrakabotService.IsServiceRunning();
            //stopKrakabotToolStripMenuItem.Enabled = KrakabotService.KrakabotService.IsServiceRunning();
        }

        private void ConfigMenuItem_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            ContentItem item = (ContentItem)menuItem.Tag;
            var proc = new Process();
            proc.StartInfo.FileName = GetExeFilespec(item.Command);
            proc.Start();
        }

        private void LoadConfigMenus(object sender, EventArgs e)
        {
            ConfigFile file = new ConfigFile();
            file.LoadFile();
            foreach (ContentItem item in file.Content.Items)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(item.Text);
                menuItem.Tag = item;
                menuItem.Click += new EventHandler(ConfigMenuItem_Clicked);
                contextMenuStrip1.Items.Insert(0, menuItem);
            }
        }
        private static string GetExePath()
        {
            return (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
        }
        private static string GetExeFilespec(string filename)
        {
            return (System.IO.Path.Combine(GetExePath(), filename));
        }
    }
}
