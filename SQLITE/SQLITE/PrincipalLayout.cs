using SQLite.Services;
using SQLITE.Models;
using SQLITE.Services;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SQLITE
{
    public partial class PrincipalLayout : Form
    {

        DataBaseConnection db;
        bool isConnect;
        public PrincipalLayout()
        {
            InitializeComponent();
            this.isConnect = false;
            this.treeViewDataConecction.NodeMouseClick += TreeViewDataConecction_NodeMouseClick;
            this.treeViewDataConecction.MouseDoubleClick += TreeViewDataConecction_MouseDoubleClick;
        }

        private async void TreeViewDataConecction_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var node = this.treeViewDataConecction.SelectedNode;
            if (node == null)
            {
                return;
            }
            var nodeInfo = (NodeInfo)node.Tag;
            if (nodeInfo == null || nodeInfo.Type != NodeType.Table)
            {
                return;
            }

            string query = $"select * from {node.Text}";
            var dataSet = await this.db.DataSet(query, node.Text);
            this.dataGridView1.DataSource = dataSet.Tables[0];
            return;
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

        private async void pictureBox2_ClickAsync(object sender, EventArgs e)
        {
            if (!this.isConnect)
            {
                try
                {
                    var result = await this.ConnectWithDatabaseAsync();
                    if (result)
                    {
                        this.isConnect = true;
                        this.pictureBox2.BackgroundImage = Image.FromFile(@"..\..\Resources\cancel.png");
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Cannot open the file selected");
                    return;
                }

            }
            else
            {
                await this.db.CloseDatabase();
                this.isConnect = false;
                this.pictureBox2.BackgroundImage = Image.FromFile(@"..\..\Resources\add.png");
                this.treeViewDataConecction.Nodes.Clear();
                dataGridView1.DataSource = null;
            }
        }

        public async Task<bool> ConnectWithDatabaseAsync()
        {
            this.db = new DataBaseConnection();
            string path = string.Empty;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
            }


            var Exist = await this.db.OpenDataBase(path);
            if (!Exist)
                return false;

            var viewList = await this.db.GetViewsDataBase();

            var tables = this.treeViewDataConecction.Nodes.Add("TABLES");
            tables.Tag = new NodeInfo
            {
                Id = 1,
                Type = NodeType.Menu
            };

            var views = this.treeViewDataConecction.Nodes.Add("VIEWS");
            views.Tag = new NodeInfo
            {
                Id = 1,
                Type = NodeType.Menu
            };



            views.Nodes.AddRange(
                viewList.Select(s => new TreeNode
                {
                    Text = s.ViewName,
                    Tag = new NodeInfo
                    {
                        Id = 0,
                        Type = NodeType.View
                    }
                }).ToArray());

            var tableList = await this.db.GetTablesDataBase();

            var t = new NodeInfo();

            //tables.Nodes.AddRange(
            //    tableList.Select(sa => new TreeNode
            //    {
            //        Text  = sa.TableName,
            //        Tag = new NodeInfo
            //        {
            //            Id = 0,
            //            Type = NodeType.table
            //        },
            //        nodes = sa.columns.select(tt => new treenode { }).toarray()
            //    }).toarray());

            foreach (var item in tableList)
            {
                var n = tables.Nodes.Add(item.TableName);
                n.Tag = Tag = new NodeInfo
                {
                    Id = 0,
                    Type = NodeType.Table
                };

                n.Nodes.AddRange(item.Columns.Select(c => new TreeNode
                {
                    Text = c.ColumnName,
                    Tag = new NodeInfo
                    {
                        Id = 0,
                        Type = NodeType.Column
                    }
                }).ToArray());
            }

            return true;
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
    }
}
