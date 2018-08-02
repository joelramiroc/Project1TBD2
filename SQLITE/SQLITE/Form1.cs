using SQLite.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace SQLITE
{
    public partial class Form1 : Form
    {

        DataBaseConnection db;

        public Form1()
        {
            InitializeComponent();
            this.treeViewDataConecction.DoubleClick += TreeViewDataConecction_DoubleClick;
            this.treeViewDataConecction.NodeMouseClick += TreeViewDataConecction_NodeMouseClick;
        }

        private void TreeViewDataConecction_NodeMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = this.treeViewDataConecction.GetNodeAt(new Point(e.X, e.Y));
                if (node.Level == 1)
                {
                    return;
                }

                this.contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void TreeViewDataConecction_DoubleClick(object sender, EventArgs e)
        {
            var node = this.treeViewDataConecction.SelectedNode;
            if (node.Level == 1)
            {
                return;
            }

            SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter($@"select * from {node.Text}", this.db.SQLiteConnection);
            DataSet dataSet = new DataSet();
            sqLiteDataAdapter.Fill(dataSet, node.Text);
            this.dataGridView1.DataSource = dataSet.Tables[0];
            return;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.ConnectWithDatabase();
        }

        public void ConnectWithDatabase()
        {
            this.db = new DataBaseConnection();
            this.db.OpenConecttion();
            //string query = @"create table if not exists test(
            //                id,
            //                name text,
            //                age number)";
            //SQLiteCommand sQLiteCommand = new SQLiteCommand(query, this.db.SQLiteConnection);
            //var result = sQLiteCommand.ExecuteNonQuery();

            //string query2 = @"insert into test values(2,'MARLON',14)";
            //SQLiteCommand sQLiteCommand2 = new SQLiteCommand(query2, this.db.SQLiteConnection);
            //var result2 = sQLiteCommand2.ExecuteNonQuery();


            string query3 = @"select * FROM sqlite_master WHERE type ='table'";
            SQLiteCommand sQLiteCommand3 = new SQLiteCommand(query3, this.db.SQLiteConnection);
            var resultRead = sQLiteCommand3.ExecuteReader();

            while (resultRead.Read())
            {
                var leido = resultRead["name"];

                string query4 = $@"PRAGMA table_info({leido})";
                SQLiteCommand sQLiteCommand4 = new SQLiteCommand(query4, this.db.SQLiteConnection);
                var resultRead2 = sQLiteCommand4.ExecuteReader();
                var t = this.treeViewDataConecction.Nodes.Add(leido.ToString());

                while (resultRead2.Read())
                {
                    var leido2 = resultRead2["name"];
                    t.Nodes.Add(leido2.ToString());
                }

            }            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.manualQuerys.TextLength == 0)
            {
                MessageBox.Show("The query is empty");
                return;
            }

            var text = this.manualQuerys.Text;

        }
    }
}
