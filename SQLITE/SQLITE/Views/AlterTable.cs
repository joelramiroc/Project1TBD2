using SQLITE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLITE.Views
{
    public partial class AlterTable : Form
    {
        TableModel tableModel;

        public string Sql { get; set; }

        public string Query { get; set; }

        List<ColumnModel> ColumKeys;

        List<ColumnModel> ColumnDeletes;

        public AlterTable(TableModel tableModel)
        {
            InitializeComponent();
            this.tableModel = tableModel;
            this.LoadData();
            this.AddComboBox();
            this.ColumnDeletes = new List<ColumnModel>();
            this.dataGridView1.RowsAdded += DataGridView1_RowsAdded;
            this.dataGridView1.RowsRemoved += DataGridView1_RowsRemoved;
        }

        private void DataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            var column = new ColumnModel
            {
                Cid = Convert.ToInt32(row.Cells["Cid"].Value),
                ColumnName = row.Cells["ColumName"].Value.ToString(),
                DateType = row.Cells["ColumType"].Value.ToString(),
                DefaultValue = row.Cells["DefaultValue"].Value.ToString(),
                IsNull = Convert.ToBoolean(row.Cells["IsNull"].Value),
                IsPrimaryKey = Convert.ToBoolean(row.Cells["PrimaryKey"].Value)
            };

            if (this.tableModel.Columns.Exists(x => x.Cid == column.Cid))
            {
                this.ColumnDeletes.Add(column);
            }
        }

        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.AddComboBox();
        }

        public async void LoadData()
        {
            this.ColumKeys = new List<ColumnModel>();
            this.ColumKeys = this.tableModel.Columns.Where(x => x.IsPrimaryKey == true).ToList();

            foreach (var item in tableModel.Columns)
            {
                var row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Cid });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = item.ColumnName });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = item.DateType });
                row.Cells.Add(new DataGridViewCheckBoxCell { Value = item.IsNull });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = item.DefaultValue });
                row.Cells.Add(new DataGridViewCheckBoxCell { Value = item.IsPrimaryKey });
                var codeCell = new DataGridViewComboBoxCell();
                var array = (string[])Enum.GetNames(typeof(DateType));
                codeCell.Items.AddRange(array);
                row.Cells.Add(codeCell);
                this.dataGridView1.Rows.Add(row);
            }
        }

        public async void AddComboBox()
        {
            var combobox = new DataGridViewComboBoxColumn();
            var array = (string[])Enum.GetNames(typeof(DateType));
            DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["NewType"];
            cbCell.Items.AddRange(array);
            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["ColumName"].ReadOnly = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            this.Query = await this.GetQuery();
            this.ddl.Text = this.Query;
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

        private async Task<string> GetQuery()
        {
            return string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
