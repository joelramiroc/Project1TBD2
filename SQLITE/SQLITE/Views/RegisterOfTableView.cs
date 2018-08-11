using SQLITE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLITE.Views
{
    public partial class RegisterOfTableView : Form
    {
        bool isCharging;
        SQLiteConnection SQLiteConnection;
        public string TableName { get; set; }
        public string SqlErrors { get; set; }
        public DataSet DataSet { get; set; }
        int rowS;

        TableModel model;

        List<ColumnModel> columKeys;

        public RegisterOfTableView(string tableName, DataSet dataSet, TableModel model, SQLiteConnection SQLiteConnection)
        {
            InitializeComponent();
            this.TableName = tableName;
            this.SQLiteConnection = SQLiteConnection;
            this.SqlErrors = "";
            this.DataSet = dataSet;
            this.isCharging = true;
            this.columKeys = new List<ColumnModel>();
            this.model = model;
            this.LoadData();
            this.dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            this.dataGridView1.CellClick += DataGridView1_CellClick;
            this.dataGridView1.UserDeletingRow += DataGridView1_UserDeletingRow;
            this.rowS = -1;
        }

        private async void DataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var miniq = $"delete from {model.TableName} Where";
            var count = e.Row.Cells.Count;
            var t = dataGridView1.Rows[this.rowS];
            var list = new List<string>();
            for (int i = 0; i < model.Columns.Count; i++)
            {
                if (model.Columns[i].IsPrimaryKey.Value)
                {
                    list.Add(model.Columns[i].ColumnName);
                }
            }
            if (list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != 0 && i != list.Count - 1)
                    {
                        miniq += " and";
                    }
                    miniq += $" { list[i]} = { t.Cells[list[i]].Value}";
                }

            }
            else
            {
                var li = new List<string>();

                for (int i = 0; i < model.Columns.Count; i++)
                {
                    li.Add(model.Columns[i].ColumnName);

                }
                for (int i = 0; i < li.Count; i++)
                {
                    if (i != 0)
                    {
                        miniq += " and";
                    }

                    try
                    {
                        miniq += $@" { li[i]} = '{ t.Cells[li[i]].Value}'";
                    }
                    catch (Exception)
                    {
                        miniq += $@" { li[i]} = '{ t.Cells[li[i]].Value}'";
                    }
                }
            }
            var result = await this.ExecuteCommand(miniq);
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.rowS = e.RowIndex;
        }






        private async void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var miniq = $"UPDATE {model.TableName} set {this.dataGridView1.Columns[e.ColumnIndex].Name} = '{this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value}' WHERE";
            var t = dataGridView1.Rows[e.RowIndex];
            var list = new List<string>();
            for (int i = 0; i < model.Columns.Count; i++)
            {
                if (model.Columns[i].IsPrimaryKey.Value)
                {
                    list.Add(model.Columns[i].ColumnName);
                }
            }
            if (list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != 0 && i != list.Count - 1)
                    {
                        miniq += " and";
                    }
                    miniq += $" { list[i]} = { t.Cells[list[i]].Value}";
                }

            }
            else
            {
                var li = new List<string>();

                for (int i = 0; i < model.Columns.Count; i++)
                {
                    li.Add(model.Columns[i].ColumnName);

                }
                for (int i = 0; i < li.Count; i++)
                {
                    if (i != e.ColumnIndex)
                    {


                        if (i != 0)
                        {
                            miniq += " and";
                        }

                        try
                        {
                            miniq += $@" { li[i]} = '{ t.Cells[li[i]].Value}'";
                        }
                        catch (Exception)
                        {
                            miniq += $@" { li[i]} = '{ t.Cells[li[i]].Value}'";
                        }
                    }
                }
            }
            var result = await this.ExecuteCommand(miniq);
        }







        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (this.isCharging)
            {
                return;
            }
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
        }

        public void LoadData()
        {
            this.columKeys.AddRange(this.model.Columns.Where(x => x.IsPrimaryKey == true));
            this.dataGridView1.RowsAdded -= new DataGridViewRowsAddedEventHandler(DataGridView1_RowsAdded);
            this.dataGridView1.DataSource = this.DataSet.Tables[0];
            this.isCharging = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, System.EventArgs e)
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

        private void dataGridView1_AllowUserToDeleteRowsChanged(object sender, System.EventArgs e)
        {
            if (this.isCharging)
            {
                return;
            }
        }

        private void initiar_Click(object sender, EventArgs e)
        {
            this.dataGridView1.RowsAdded += new DataGridViewRowsAddedEventHandler(DataGridView1_RowsAdded);
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

        private async void button2_Click(object sender, EventArgs e)
        {
            var miniq = $"INSERT INTO {model.TableName} VALUES(";
            DataGridViewRow row = dataGridView1.Rows[this.dataGridView1.RowCount - 2];
            for (int i = 0; i < this.dataGridView1.ColumnCount; i++)
            {
                if (i != 0)
                {
                    miniq += ",";
                }
                miniq += $@" '{row.Cells[i].Value}'";
            }
            miniq += ")";
            var result = await this.ExecuteCommand(miniq);
        }
    }
}
