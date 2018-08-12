using SQLITE.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SQLITE.Views
{
    public partial class CreateTrigger : Form
    {
        public string Sql { get; set; }

        string query, lastSql;
        bool isNew;
        DatabaseModel databaseModel;


        public CreateTrigger(bool isNew, string lastSql,DatabaseModel databaseModel)
        {
            InitializeComponent();
            this.databaseModel = databaseModel;
            this.isNew = isNew;
            this.lastSql = @"BEGIN
                            statements
                            END";
            this.LoadBasicSqlAndComboBox();
        }


        private async void LoadBasicSqlAndComboBox()
        {
            if (this.isNew)
            {
                this.button1.Text = "Edit";
                this.comboTable.Items.AddRange(this.databaseModel.Tables.Select(x => x.TableName).ToArray());
                this.comboAction.Items.AddRange((string[])Enum.GetNames(typeof(TriggerType)));
                this.combWhen.Items.AddRange((string[])Enum.GetNames(typeof(TriggerWhen)));
            }
            else
            {
                this.ddl.Text = this.query;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var query = $"CREATE TRIGGER IF NOT EXISTS {this.triggerName.Text}" +
                $" {this.comboAction.Text}" +
                $" {this.combWhen.Text}" +
                $" ON {this.comboTable.Text}" +
                $" WHEN {this.texboxCondition.Text}" +
                $" BEGIN \n" +
                $" {this.ddl.Text}; \n" +
                $" END;";
            this.Sql = query;
            this.ddl.Text = query;

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
