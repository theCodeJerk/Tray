using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using theCodeJerk.Tray.Config;

namespace theCodeJerk.Tray.Editor
{
    public partial class MainForm : Form
    {
        private ConfigFile _ConfigFile = new ConfigFile();

        public MainForm()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ConfigFile = new ConfigFile();
            listView1.Items.Clear();
            toolStripStatusLabel1.Text = "New File";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel1.Text = openFileDialog1.FileName;
                _ConfigFile = new ConfigFile();
                _ConfigFile.LoadFile(openFileDialog1.FileName);
                listView1.Items.Clear();
                List<ContentItem> items = _ConfigFile.Content.Items;
                foreach(ContentItem item in items)
                {
                   ListViewItem listViewItem = listView1.Items.Add(item.Text);
                    listViewItem.SubItems.Add(item.Command);
                    listViewItem.Tag = item;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolStripStatusLabel1.Text == "New File")
            {
                saveAsToolStripMenuItem_Click(sender, e);
            } else
            {
                _ConfigFile.SaveFile(toolStripStatusLabel1.Text);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                _ConfigFile.SaveFile(saveFileDialog1.FileName);
                toolStripStatusLabel1.Text = saveFileDialog1.FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemEditor dialog = new ItemEditor();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _ConfigFile.Content.Items.Add(dialog.Item);
                ListViewItem listViewItem = listView1.Items.Add(dialog.Item.Text);
                listViewItem.SubItems.Add(dialog.Item.Command);
                listViewItem.Tag = dialog.Item;
            }
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ItemEditor dialog = new ItemEditor();
                dialog.Item = listView1.SelectedItems[0].Tag as ContentItem;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _ConfigFile.Content.Items.Remove(listView1.SelectedItems[0].Tag as ContentItem);
                    listView1.SelectedItems[0].Text = dialog.Item.Text;
                    listView1.SelectedItems[0].SubItems[0].Text = dialog.Item.Command;
                    listView1.SelectedItems[0].Tag = dialog.Item;
                    _ConfigFile.Content.Items.Add(dialog.Item);
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                _ConfigFile.Content.Items.Remove(listView1.SelectedItems[0].Tag as ContentItem);
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
        }
    }
}
