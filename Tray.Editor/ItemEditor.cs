using System;
using System.Windows.Forms;
using theCodeJerk.Tray.Config;

namespace theCodeJerk.Tray.Editor
{
    public partial class ItemEditor : Form
    {
        public ItemEditor()
        {
            InitializeComponent();
        }
        public ContentItem Item
        {
            get
            {
                ContentItem item = new ContentItem();
                item.Text = textBox1.Text;
                item.Command = textBox2.Text;
                return item;
            }
            set
            {
                textBox1.Text = value.Text;
                textBox2.Text = value.Command;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }
    }
}
