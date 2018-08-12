using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLITE.Views
{
    public partial class CreateDatabase : Form
    {
        public string DatabasePath { get; set; }
        public string DatabaseName { get; set; }
        public CreateDatabase()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.textBox1.Text))
            {
                this.DatabaseName = this.textBox1.Text;
                string caption = "Advertence";
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.DatabasePath= folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    return;
                }
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show("Are you sure", caption, buttons);
                if (result == DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
