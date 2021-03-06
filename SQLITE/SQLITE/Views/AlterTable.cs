﻿using SQLITE.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLITE.Views
{
    public partial class AlterTable : Form
    {
        TableModel tableModel;

        public string Sql { get; set; }

        public string SqlErrors { get; set; }

        public string Query { get; set; }

        SQLiteConnection SQLiteConnection;

        List<ColumnModel> columEdites;
        List<ColumnModel> columFinallyEdites;
        List<ColumnModel> ColumnDeletes;
        List<ColumnModel> ColumnAdds;
        List<ColumnModel> ColumnFinally;
        public List<string> Querys;

        public AlterTable(TableModel tableModel, SQLiteConnection SQLiteConnection)
        {
            InitializeComponent();
            this.SQLiteConnection = SQLiteConnection;
            this.tableModel = tableModel;
            this.LoadData();
            this.AddComboBox();
            this.ColumnDeletes = new List<ColumnModel>();
            this.ColumnAdds = new List<ColumnModel>();
            this.ColumnFinally = new List<ColumnModel>();
            this.columFinallyEdites = new List<ColumnModel>();
            this.Querys = new List<string>();
            this.dataGridView1.RowsAdded += DataGridView1_RowsAdded;
            this.dataGridView1.UserDeletedRow += DataGridView1_UserDeletedRow;
            this.dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            this.SqlErrors = "";
        }

        private void DataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            DataGridViewRow row = e.Row;
            var column = new ColumnModel();
            column.ColumnName = row.Cells[1].Value.ToString();
            column.IsNull = Convert.ToBoolean(row.Cells[3].Value);
            column.DefaultValue = row.Cells[4].Value.ToString();
            column.DateType = row.Cells[2].Value.ToString();
            column.IsPrimaryKey = Convert.ToBoolean(row.Cells[5].Value);



            if (this.tableModel.Columns.Exists(x => x.ColumnName == column.ColumnName))
            {
                this.ColumnDeletes.Add(column);
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            var columName = this.dataGridView1.Rows[e.RowIndex].Cells[ColumName.Name].Value?.ToString();
            var columType = this.dataGridView1.Rows[e.RowIndex].Cells[ColumType.Name].Value?.ToString();
            var ncolumType = this.dataGridView1.Rows[e.RowIndex].Cells[NewType.Name].Value?.ToString();
            var columPk = Convert.ToBoolean(this.dataGridView1.Rows[e.RowIndex].Cells[PrimaryKey.Name].Value);
            var columIsNull = Convert.ToBoolean(this.dataGridView1.Rows[e.RowIndex].Cells[IsNull.Name].Value);
            var columdefaultValue = this.dataGridView1.Rows[e.RowIndex].Cells[DefaultValue.Name].Value?.ToString();
            var valueasd = (this.dataGridView1.Rows[e.RowIndex].Cells[Cid.Name].Value);
            int? columCid = (this.dataGridView1.Rows[e.RowIndex].Cells[Cid.Name].Value) == null ? -1 : Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[Cid.Name].Value);

            if (columCid == -1)
            {
                return;
            }

            if (!String.IsNullOrEmpty(columName))
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

                if (!String.IsNullOrEmpty(ncolumType))
                {
                    columnModel.DateType = ncolumType;
                }
                this.columEdites.Add(columnModel);
            }
        }

        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.AddComboBox();
        }

        public async void LoadData()
        {
            this.oldTableName.Text = this.tableModel.TableName;
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
            this.Query = "";
            if (!String.IsNullOrEmpty(this.newTableName.Text) && !this.tableModel.TableName.Equals(this.newTableName.Text))
            {
                this.Query = (await this.AlterNameTable()) + " \n "; ;
            }
            await GetColumnFinnally();
            if (this.columEdites.Count != 0)
            {
                this.Query = (await this.AlterColumn()) + " \n ";
                this.tableModel.Columns = this.columFinallyEdites;
            }
            if (this.ColumnDeletes.Count != 0)
            {
                this.Query += (await this.AlterColumnDeleted()) + " \n ";
            }
            if (this.ColumnAdds.Count != 0)
            {
                this.Query += (await this.AlterColumnAdd()) + " \n ";
            }

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
            if (this.dataGridView1.RowCount - 1 > this.tableModel.Columns.Count)
            {
                for (int i = this.tableModel.Columns.Count; i < dataGridView1.RowCount - 1; i++)
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

        private async Task<string> AlterNameTable()
        {
            var miniQuery = $"ALTER TABLE {this.tableModel.TableName} RENAME TO {this.newTableName.Text}";
            var result = await this.ExecuteCommand(miniQuery);
            if (result)
            {
                this.tableModel.TableName = this.newTableName.Text;
            }
            this.Querys.Add(miniQuery);
            return miniQuery;
        }

        private async Task<string> AlterColumn()
        {
            var miniQ = "";
            var miniQuery = "PRAGMA foreign_keys=off";
            miniQuery = $"ALTER TABLE {this.tableModel.TableName} RENAME TO {this.tableModel.TableName}_old";
            miniQ += miniQuery + " \n ";
            await this.ExecuteCommand(miniQuery);
            miniQuery = $"CREATE TABLE {this.tableModel.TableName}( ";
            miniQ += miniQuery + " \n ";
            await this.GetColumnFinallyEdited();
            var keys = new List<string>();
            bool haveKeys = false;
            for (int i = 0; i < this.columFinallyEdites.Count; i++)
            {
                if (i != 0)
                {
                    miniQuery += ", ";
                }
                if (columFinallyEdites[i].IsPrimaryKey != null && columFinallyEdites[i].IsPrimaryKey.Value)
                {
                    haveKeys = true;
                    keys.Add(columFinallyEdites[i].ColumnName);
                }
                miniQuery += $"{await this.GetColumnQuery(columFinallyEdites[i])}";
            }
            if (haveKeys)
            {
                miniQuery += ", PRIMARY KEY(";
                for (int i = 0; i < keys.Count; i++)
                {
                    if (i != 0)
                    {
                        miniQuery += ",";
                    }

                    miniQuery += $" {keys[i]}";
                }
                miniQuery += ")";
            }
            miniQuery += ")";
            miniQ += miniQuery + " \n ";
            await this.ExecuteCommand(miniQuery);
            var listCam = await this.GetListColumnsE();
            miniQuery = $"INSERT INTO {this.tableModel.TableName}" +
                $"({listCam})" +
                $" select {listCam} from {this.tableModel.TableName}_old";
            await this.ExecuteCommand(miniQuery);
            miniQ += miniQuery + " \n ";
            miniQuery = $"DROP TABLE {this.tableModel.TableName}_old";
            await this.ExecuteCommand(miniQuery);
            miniQ += miniQuery + " \n ";
            //COMMIT;
            //PRAGMA foreign_keys = on;
            return miniQ;
        }

        private async Task<string> AlterColumnDeleted()
        {
            var miniQ = "";
            var miniQuery = "PRAGMA foreign_keys=off";
            await this.GetColumnFinnally();
            miniQuery = $"ALTER TABLE {this.tableModel.TableName} RENAME TO {this.tableModel.TableName}_old";
            miniQ += miniQuery + " \n ";
            await this.ExecuteCommand(miniQuery);
            miniQuery = $"CREATE TABLE {this.tableModel.TableName}( ";
            miniQ += miniQuery + " \n ";
            var keys = new List<string>();
            bool haveKeys = false;
            for (int i = 0; i < this.ColumnFinally.Count; i++)
            {
                if (i != 0)
                {
                    miniQuery += ", ";
                }
                if (ColumnFinally[i].IsPrimaryKey != null && ColumnFinally[i].IsPrimaryKey.Value)
                {
                    haveKeys = true;
                    keys.Add(ColumnFinally[i].ColumnName);
                }
                miniQuery += $"{await this.GetColumnQuery(ColumnFinally[i])}";
            }
            if (haveKeys)
            {
                miniQuery += ", PRIMARY KEY(";
                for (int i = 0; i < keys.Count; i++)
                {
                    if (i != 0)
                    {
                        miniQuery += ",";
                    }

                    miniQuery += $" {keys[i]}";
                }
                miniQuery += ")";
            }
            miniQuery += ")";
            miniQ += miniQuery + " \n ";
            await this.ExecuteCommand(miniQuery);
            var listCam = await this.GetListColumns();
            miniQuery = $"INSERT INTO {this.tableModel.TableName} (" +
                $"{listCam})" +
                $" select {listCam} from {this.tableModel.TableName}_old";
            await this.ExecuteCommand(miniQuery);
            miniQ += miniQuery + " \n ";
            miniQuery = $"DROP TABLE {this.tableModel.TableName}_old";
            await this.ExecuteCommand(miniQuery);
            miniQ += miniQuery + " \n ";
            //COMMIT;
            //PRAGMA foreign_keys = on;
            return miniQ;
        }

        private async Task<string> AlterColumnAdd()
        {
            var miniQuery = "";
            for (int i = 0; i < this.ColumnAdds.Count; i++)
            {
                if (i == 0)
                {
                    miniQuery = $"ALTER TABLE {this.tableModel.TableName} ADD {await this.GetColumnQuery(ColumnAdds[i])}";
                }
                else
                {
                    if (i != 0)
                    {
                        miniQuery += ", ";
                    }
                    miniQuery += $"{await this.GetColumnQuery(ColumnAdds[i])}";
                }
            }
            this.Querys.Add(miniQuery);
            await this.ExecuteCommand(miniQuery);
            return miniQuery;
        }

        private async Task<string> GetColumnQuery(ColumnModel model)
        {
            var miniQuery = $"{model.ColumnName} {model.DateType}";
            if (model.IsPrimaryKey != null && model.IsPrimaryKey.Value)
            {
            }
            else
            {
                if (!String.IsNullOrEmpty(model.DefaultValue))
                {
                    miniQuery += $" DEFAULT { model.DefaultValue}";
                }
                if (model.IsNull.Value)
                {
                    miniQuery += $" NOT NULL";
                }
            }
            return miniQuery;
        }

        private async Task<string> GetQuery()
        {
            return string.Empty;
        }

        private async Task<string> GetListColumns()
        {
            string list = "";
            for (int i = 0; i < this.ColumnFinally.Count; i++)
            {
                if (i != 0)
                {
                    list += ", ";
                }
                list += $"{this.ColumnFinally[i].ColumnName}";
            }
            return list;
        }

        private async Task<string> GetListColumnsE()
        {
            string list = "";
            for (int i = 0; i < this.columFinallyEdites.Count; i++)
            {
                if (i != 0)
                {
                    list += ", ";
                }
                list += $"{this.columFinallyEdites[i].ColumnName}";
            }
            return list;
        }

        private async Task<bool> GetColumnFinnally()
        {
            this.ColumnFinally = new List<ColumnModel>();
            foreach (var item in this.tableModel.Columns)
            {
                if (this.ColumnDeletes.Find(x => x.ColumnName.Equals(item.ColumnName)) == null)
                {
                    this.ColumnFinally.Add(item);
                }
            }

            return true;
        }

        private async Task<bool> GetColumnFinallyEdited()
        {
            foreach (var item in this.columEdites)
            {
                if (this.ColumnFinally.Find(x => x.ColumnName.Equals(item.ColumnName)) != null)
                {
                    this.columFinallyEdites.Add(item);
                }
            }

            foreach (var item in this.ColumnFinally)
            {
                if (this.columEdites.Find(x => x.ColumnName.Equals(item.ColumnName)) == null)
                {
                    this.columFinallyEdites.Add(item);
                }
            }
            return true;
        }

        private async Task<bool> ExecuteCommand(string query)
        {
            try
            {
                SQLiteCommand sQLiteCommand = new SQLiteCommand(query, this.SQLiteConnection);
                var result = sQLiteCommand.ExecuteNonQuery();
            }
            catch (Exception exs)
            {
                this.SqlErrors += exs.Message + " \n";
                return false;
            }
            return true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
