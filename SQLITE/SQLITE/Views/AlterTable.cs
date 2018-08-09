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

        List<ColumnModel> columEdites;

        List<ColumnModel> ColumnDeletes;
        List<ColumnModel> ColumnAdds;

        public AlterTable(TableModel tableModel)
        {
            InitializeComponent();
            this.tableModel = tableModel;
            this.LoadData();
            this.AddComboBox();
            this.ColumnDeletes = new List<ColumnModel>();
            this.ColumnAdds = new List<ColumnModel>();
            this.dataGridView1.RowsAdded += DataGridView1_RowsAdded;
            this.dataGridView1.RowsRemoved += DataGridView1_RowsRemoved;
            this.dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }


        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            var columName = this.dataGridView1.Rows[e.RowIndex].Cells[ColumName.Name].Value?.ToString();
            var columType = this.dataGridView1.Rows[e.RowIndex].Cells[ColumType.Name].Value?.ToString();
            var columPk = Convert.ToBoolean(this.dataGridView1.Rows[e.RowIndex].Cells[PrimaryKey.Name].Value);
            var columIsNull = Convert.ToBoolean(this.dataGridView1.Rows[e.RowIndex].Cells[IsNull.Name].Value);
            var columdefaultValue = this.dataGridView1.Rows[e.RowIndex].Cells[DefaultValue.Name].Value?.ToString();
            var valueasd = (this.dataGridView1.Rows[e.RowIndex].Cells[Cid.Name].Value);
            int? columCid = (this.dataGridView1.Rows[e.RowIndex].Cells[Cid.Name].Value)  == null ? -1 : Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[Cid.Name].Value);

            if (columCid == -1)
            {
                return;
            }

            if (!String.IsNullOrEmpty(columName) && !String.IsNullOrEmpty(columType))
            {
                var columnModel = new ColumnModel
                {
                    ColumnName = columName,
                    DateType = columType,
                    DefaultValue = columdefaultValue,
                    IsNull = columIsNull,
                    IsPrimaryKey = columPk,
                    Cid = columCid
                };
                this.columEdites.Add(columnModel);
            }
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
            this.columEdites = new List<ColumnModel>();
            this.columEdites = this.tableModel.Columns.Where(x => x.IsPrimaryKey == true).ToList();

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
            this.GetNewColumns();
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

        private bool GetNewColumns()
        {
            if (this.dataGridView1.RowCount-1 > this.tableModel.Columns.Count)
            {
                for (int i = this.tableModel.Columns.Count; i < dataGridView1.RowCount-1; i++)
                {
                    var columName = this.dataGridView1.Rows[i].Cells[ColumName.Name].Value?.ToString();
                    var columType = this.dataGridView1.Rows[i].Cells[NewType.Name].Value?.ToString();
                    var columPk = Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[PrimaryKey.Name].Value);
                    var columIsNull = Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[IsNull.Name].Value);
                    var columdefaultValue = this.dataGridView1.Rows[i].Cells[DefaultValue.Name].Value?.ToString();
                    if (!String.IsNullOrEmpty(columName) && !String.IsNullOrEmpty(columType))
                    {
                        var columnModel = new ColumnModel
                        {
                            ColumnName = columName,
                            DateType = columType,
                            DefaultValue = columdefaultValue,
                            IsNull = columIsNull,
                            IsPrimaryKey = columPk
                        };
                        this.ColumnAdds.Add(columnModel);
                    }
                }
            }
            return true;
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
