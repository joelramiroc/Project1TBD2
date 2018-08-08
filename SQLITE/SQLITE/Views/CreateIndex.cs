using SQLITE.Models;
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
    public partial class CreateIndex : Form
    {
        public string Sql { get; set; }
        bool isNew;
        string indexNameS;
        public DatabaseModel DatabaseModel { get; set; }

        public CreateIndex(bool isNew, string indexName, DatabaseModel datab)
        {
            InitializeComponent();
            this.DatabaseModel = datab;
            this.isNew = isNew;
            this.indexNameS = indexName;
            this.LoadCombobox();
        }

        public async void LoadCombobox()
        {
            foreach (var item in this.DatabaseModel.Tables)
            {
                this.tables.Items.Add(item.TableName);
            }
            //this.tables.Items.AddRange(this.DatabaseModel.Tables.Select(x => new ListViewItem
            //{
            //    Text = x.TableName,
            //    Tag = x
            //}).ToArray());
        }

        private void tables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.indexName.Text))
            {
                MessageBox.Show("Write a name to the INDEX");
                return;
            }

            var tableSelected = this.tables.Text.ToString();
            var columnSelected = this.columns.Text.ToString();

            if (String.IsNullOrEmpty(tableSelected) || String.IsNullOrEmpty(columnSelected))
            {
                return;
            }

            var query = "CREATE ";
            if (this.checkBox1.CheckState == CheckState.Checked)
            {
                query += "UNIQUE ";
            }
            query += $"INDEX {this.indexName.Text} ON {tableSelected}({columnSelected})";
            this.ddl.Text = query;
            this.Sql = query;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.columns.Items.Clear();
            var tab = this.tables.SelectedItem;
            var tables = this.DatabaseModel.Tables.Find(x => x.TableName.Equals(tab));
            if (tables == null)
            {
                return;
            }
            foreach (var item in tables.Columns)
            {
                this.columns.Items.Add(item.ColumnName);
            }
        }
    }
}
