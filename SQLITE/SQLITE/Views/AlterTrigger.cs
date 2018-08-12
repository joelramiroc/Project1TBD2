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
    public partial class AlterTrigger : Form
    {
        public string Sql { get; set; }
        public AlterTrigger(string command)
        {
            InitializeComponent();
            this.Sql = command;
            this.manualQuerys.Text = command;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.manualQuerys.Text))
            {
                this.Sql = this.manualQuerys.Text;
                string caption = "Advertence";
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
