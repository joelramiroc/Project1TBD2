using System;
using System.Windows.Forms;

namespace SQLITE.Views
{
    public partial class CreateView : Form
    {
        public string Sql { get; set; }

        bool isNew;
        string query,lastQuery;

        public CreateView(bool isNew, string lastQuery)
        {
            this.lastQuery = lastQuery;
            this.isNew = isNew;
            InitializeComponent();
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
            if (this.isNew)
            {
                this.query = "Create view [VIEWNAME] \n" +
                "AS \n \n" +
                "";
                this.button1.Text = "Edit";
            }
            else
            {
                this.query = this.lastQuery;
            }

            this.ddl.Text = this.query;
        }
    }
}
