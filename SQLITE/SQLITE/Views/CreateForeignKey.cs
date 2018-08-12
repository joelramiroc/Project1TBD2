using SQLITE.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLITE.Views
{
    public partial class CreateForeignKey : Form
    {
        DatabaseModel databaseModel;
        string tableName,sqlErrors,td,cd,co;
        SQLiteConnection sQLiteConnection;
        List<ColumnModel> columFinallyEdites;
        public CreateForeignKey(DatabaseModel databaseModel, string tableName, SQLiteConnection sQLiteConnection)
        {
            InitializeComponent();
            this.columFinallyEdites = new List<ColumnModel>();
            this.databaseModel = databaseModel;
            this.tableName = tableName;
            this.LoadCombo();

            this.sQLiteConnection = sQLiteConnection;
        }

        private async void LoadCombo()
        {
            this.columns.Items.Clear();
            var tables = this.databaseModel.Tables.Find(x => x.TableName.Equals(this.tableName));
            this.columFinallyEdites.AddRange(tables.Columns);
            if (tables == null)
            {
                return;
            }
            foreach (var item in tables.Columns)
            {
                if (!item.IsPrimaryKey.Value)
                {
                    this.columns.Items.Add(item.ColumnName);
                }
            }
            foreach (var item in this.databaseModel.Tables)
            {
                this.tablesDestino.Items.Add(item.TableName);
            }
        }

        private void tablesDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = this.tablesDestino.Text.ToString();
            this.ColumnsDestino.Items.Clear();
            var tables = this.databaseModel.Tables.Find(x => x.TableName.Equals(selected));
            if (tables == null || tables.TableName.Equals(this.tableName))
            {
                return;
            }
            foreach (var item in tables.Columns)
            {
                this.ColumnsDestino.Items.Add(item.ColumnName);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var tabD = this.tablesDestino.Text.ToString();
            var columnD = this.ColumnsDestino.Text.ToString();
            var columnO = this.columns.Text.ToString();
            this.td = tabD;
            this.cd = columnD;
            this.co = columnO;
            this.ddl.Text = await this.AlterColumn();
            if (!String.IsNullOrEmpty(tabD) && !String.IsNullOrEmpty(columnD) && !String.IsNullOrEmpty(columnO))
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

        private async Task<bool> ExecuteCommand(string query)
        {
            try
            {
                SQLiteCommand sQLiteCommand = new SQLiteCommand(query, this.sQLiteConnection);
                var result = sQLiteCommand.ExecuteNonQuery();
            }
            catch (Exception exs)
            {
                this.sqlErrors+= exs.Message + " \n";
                return false;
            }
            return true;
        }


        private async Task<string> AlterColumn()
        {
            var miniQ = "";
            var miniQuery = "PRAGMA foreign_keys=off";
            miniQuery = $"ALTER TABLE {this.tableName} RENAME TO {this.tableName}_old";
            miniQ += miniQuery + " \n ";
            await this.ExecuteCommand(miniQuery);
            miniQuery = $"CREATE TABLE {this.tableName}( ";
            miniQ += miniQuery + " \n ";
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
                miniQuery += "), ";
            }
            miniQuery += $@"CONSTRAINT fk_{this.td}"+
                         $" FOREIGN KEY({this.co}) references {this.td}({this.cd})";
            miniQuery += ")";
            miniQ += miniQuery + " \n ";
            await this.ExecuteCommand(miniQuery);
            var listCam = await this.GetListColumnsE();
            miniQuery = $"INSERT INTO {this.tableName}" +
                $"({listCam})" +
                $" select {listCam} from {this.tableName}_old";
            await this.ExecuteCommand(miniQuery);
            miniQ += miniQuery + " \n ";
            miniQuery = $"DROP TABLE {this.tableName}_old";
            await this.ExecuteCommand(miniQuery);
            miniQ += miniQuery + " \n ";
            //COMMIT;
            //PRAGMA foreign_keys = on;
            return miniQ;
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
    }
}
