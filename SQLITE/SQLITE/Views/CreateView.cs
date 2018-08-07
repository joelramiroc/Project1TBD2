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
    public partial class CreateView : Form
    {
        public string Sql { get; set; }

        string query;

        public CreateView(bool isNew)
        {
            InitializeComponent();
            this.query = "Create view [VIEWNAME] \n" +
                "AS \n \n" +
                "";

            this.LoadBasicSql();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Sql = this.ddl.Text;
            if (this.Sql.Length < 12)
            {
                return;
            }
            else
            {
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

        private async void LoadBasicSql()
        {
            this.ddl.Text = this.query;
        }
    }
}
