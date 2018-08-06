using SQLite.Services;
using SQLITE.Models;
using SQLITE.Services;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLITE.Controller
{
    class PrincipalLayoutController
    {
        DataBaseConnection db;
        bool isConnect;
        DatabaseModel database;

        public DatabaseModel DatabaseModel { get; set; }

        public PrincipalLayoutController()
        {
            this.DatabaseModel = new DatabaseModel();
            this.db = new DataBaseConnection();

        }

        public async Task<bool> CloseDataBase()
        {
            return await this.db.CloseDatabase();
        }

        public async Task<bool> OpenDataBase(string path)
        {
            return await this.db.OpenDataBase(path);
        }

        public async Task<DatabaseModel> GethDataBase()
        {
            this.database = new DatabaseModel();
            this.database.Triggers = await this.db.GetTriggersDataBase();
            this.database.Tables = await this.db.GetTablesDataBase();
            this.database.Views = await this.db.GetViewsDataBase();
            return database;
        }

        public async Task<List<TreeNode>> GethTreeNodes()
        {
            var datab = await this.GethDataBase();

            List<TreeNode> treeNodes = new List<TreeNode>();
            var tables = new TreeNode("TABLES")
            {
                ImageKey = "carpeta cerrada",
                Tag = new NodeInfo
                {
                    Id = 1,
                    Type = NodeType.Menu
                }
            };

            treeNodes.Add(tables);



            var views = new TreeNode("VIEWS")
            {
                ImageKey = "carpeta cerrada",
                Tag = new NodeInfo
                {
                    Id = 1,
                    Type = NodeType.Menu
                }
            };
            treeNodes.Add(views);

            var triggers = new TreeNode("TRIGGERS")
            {
                ImageKey = "carpeta cerrada",
                Tag = new NodeInfo
                {
                    Id = 1,
                    Type = NodeType.Menu
                }
            };

            treeNodes.Add(triggers);

            triggers.Nodes.AddRange(
                datab.Triggers.Select(S => new TreeNode
                {
                    Text = S.TriggerName,
                    Tag = new NodeInfo
                    {
                        Id = 0,
                        Type = NodeType.Trigger
                    }
                }).ToArray()
                );

            views.Nodes.AddRange(
                            datab.Views.Select(s => new TreeNode
                            {
                                Text = s.ViewName,
                                Tag = new NodeInfo
                                {
                                    Id = 0,
                                    Type = NodeType.View
                                }
                            }).ToArray());

            tables.Nodes.AddRange(
                    datab.Tables.Select(sa =>
                    new TreeNode(sa.TableName, sa.Columns.Select(tt => new TreeNode
                    {
                        Text = tt.ColumnName,
                        Tag = new NodeInfo
                        {
                            Id = 0,
                            Type = NodeType.Column
                        }
                    }).ToArray())
                    {
                        Tag = new NodeInfo
                        {
                            Id = 0,
                            Type = NodeType.Table
                        }
                    }
                ).ToArray());


            return treeNodes;
        }

        public async Task<DataSet> DataSet(string query, string text)
        {
            return await this.db.DataSet(query,text);
        }

        public async Task<SQLiteDataReader> Consulta(string query)
        {
            return await this.db.ConsultaLectura(query);
        }

        public async Task<int> ExecuteConsult(string query)
        {
            return await this.db.ExecuteQuery(query);
        }
    }
}
