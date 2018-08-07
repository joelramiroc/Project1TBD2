using System;
using System.Windows.Forms;

namespace SQLITE.Views
{
    public partial class CreateTrigger : Form
    {
        public string Sql { get; set; }

        string query, lastSql;
        bool isNew;

        public CreateTrigger(bool isNew, string lastSql)
        {
            InitializeComponent();
            this.query = @" CREATE TRIGGER[IF NOT EXISTS] trigger_name \n"+
                "[BEFORE | AFTER | INSTEAD OF][INSERT | UPDATE | DELETE] \n"+
                "ON table_name \n"+
                "[WHEN condition] \n"+
                "BEGIN \n"+
                "statements; \n"+
                "END; \n";
            this.isNew = isNew;
            this.lastSql = lastSql;
            this.LoadBasicSql();
        }


        private async void LoadBasicSql()
        {
            if (this.isNew)
            {
                this.ddl.Text = this.lastSql;
                this.button1.Text = "Edit";
            }
            else
            {
                this.ddl.Text = this.query;
            }
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
    }
}
