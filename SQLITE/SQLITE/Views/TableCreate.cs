using SQLITE.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations.Model;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLITE.Views
{
    public partial class TableCreate : Form
    {
        public string Query { get; set; }

        public string TableName { get; set; }

        public TableCreate()
        {
            InitializeComponent();
            this.AddComboBox();

            this.dataGridView1.RowsAdded += DataGridView1_RowsAdded;
        }

        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.AddComboBox();
        }

        public async void AddComboBox()
        {
            var combobox = new DataGridViewComboBoxColumn();
            var array = (string[])Enum.GetNames(typeof(DateType));
            DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[1];
            cbCell.Items.AddRange(array);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.tableName.Text))
            {
                MessageBox.Show("Write a name to the table");
                return;
            }
            if (this.dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Write one column minim");
                return;
            }
            this.TableName = this.tableName.Text;
            this.Query = await this.GetQuery();
            this.ddl.Text = this.Query;
            string caption = "Advertence";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show("Are you sure", caption, buttons);
            if(result == DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public async Task<string>GetQuery()
        {
            string query = $"Create table {this.tableName.Text} ( ";
            List<String> columns = new List<string>();

            for (int i = 0; i < this.dataGridView1.RowCount - 1; i++)
            {
                var data = new Models.ColumnModel();
                var name = this.dataGridView1.Rows[i].Cells["ColumName"].Value?.ToString();
                if (!String.IsNullOrEmpty(name))
                {
                    data.ColumnName = name;
                }
                var datet = this.dataGridView1.Rows[i].Cells["ColumType"].Value?.ToString();
                if (!String.IsNullOrEmpty(datet))
                {
                    data.DateType = datet;
                }
                var key = this.dataGridView1.Rows[i].Cells["PrimaryKey"].Value;
                if (key != null)
                {
                    data.IsPrimaryKey = (bool)key;
                }
                var nulls = this.dataGridView1.Rows[i].Cells["IsNull"].Value;
                if (nulls != null)
                {
                    data.IsNull = (bool)nulls;
                }
                var defaultValue = this.dataGridView1.Rows[i].Cells["DefaultValue"].Value?.ToString();

                if (!String.IsNullOrEmpty(defaultValue))
                {
                    data.DefaultValue = defaultValue;
                }
                if (!String.IsNullOrEmpty(data.ColumnName) && !String.IsNullOrEmpty(data.DateType))
                {
                    var column = $"{data.ColumnName} {data.DateType} ";
                    if (data.IsPrimaryKey != null && data.IsPrimaryKey.Value)
                    {
                        column += $" PRIMARY KEY";
                    }
                    else
                    {
                        if (data.IsNull != null && !data.IsNull.Value)
                        {
                            column += $" NOT NULL";
                        }

                        if (!String.IsNullOrEmpty(data.DefaultValue))
                        {
                            column += $" DEFAULT {data.DefaultValue}";
                        }
                    }
                    if (i < this.dataGridView1.RowCount - 2)
                    {
                        column += $", ";
                    }
                    query += column;
                }
            }
            query += " )";
            return query;
        }

        private void TableCreate_Load(object sender, EventArgs e)
        {

        }

        private void ddl_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
