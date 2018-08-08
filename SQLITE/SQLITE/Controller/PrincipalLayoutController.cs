﻿using SQLite.Services;
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
        public DatabaseModel datab;
        public TreeNode TreeNodeTables;
        public TreeNode TreeNodeViews;
        public TreeNode TreeNodeTriggers;

        public DatabaseModel DatabaseModel { get; set; }

        public PrincipalLayoutController()
        {
            this.DatabaseModel = new DatabaseModel();
            this.db = new DataBaseConnection();
            this.TreeNodeTables = new TreeNode();
            this.TreeNodeViews = new TreeNode();
            this.TreeNodeTriggers = new TreeNode();
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
            this.datab = new DatabaseModel();
            this.datab.Triggers = await this.db.GetTriggersDataBase();
            this.datab.Tables = await this.db.GetTablesDataBase();
            //foreach (var item in this.datab.Tables)
            //{
            //    try
            //    {
            //        item.Indexs = await this.db.GetIndexTable(item.TableName);
            //    }
            //    catch (System.Exception es)
            //    {
            //    }
            //}
            this.datab.Views = await this.db.GetViewsDataBase();
            return datab;
        }

        public async Task<List<TreeNode>> GethTreeNodes()
        {
            await this.GethDataBase();

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
            this.TreeNodeTables = tables;
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

            this.TreeNodeViews.Nodes.Add(views);
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
            this.TreeNodeTriggers.Nodes.Add(triggers);

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

            foreach (var item in datab.Tables)
            {
                var column = new TreeNode
                {
                    Text = "COLUMNS",
                    Tag = new NodeInfo
                    {
                        Id = 0,
                        Type = NodeType.Menu
                    }
                };
                column.Nodes.AddRange(item.Columns.Select(x => new TreeNode
                {
                    Text = x.ColumnName,
                    Tag = new NodeInfo
                    {
                        Id = 0,
                        Type = NodeType.Column
                    }
                }).ToArray()); 

                var indexs = new TreeNode
                {
                    Text = "INDEXS",
                    Tag = new NodeInfo
                    {
                        Id = 0,
                        Type = NodeType.Menu
                    }
                };
                indexs.Nodes.AddRange(item.Indexs.Select(x => new TreeNode
                {
                    Text = x.IndexNamenName,
                    Tag = new NodeInfo
                    {
                        Id = 0,
                        Type = NodeType.Index
                    }
                }).ToArray());
                var tab = new TreeNode
                {
                    Text = item.TableName,
                    Tag = new NodeInfo
                    {
                        Id = 0,
                        Type = NodeType.Table
                    }
                };
                this.TreeNodeTables.Nodes.Add(tab);
                tab.Nodes.Add(column);
                tab.Nodes.Add(indexs);
            }

            //tables.Nodes.AddRange(
            //        datab.Tables.Select(sa =>
            //        new TreeNode(sa.TableName, sa.Columns.Select(tt => new TreeNode
            //        {
            //            Text = tt.ColumnName,
            //            Tag = new NodeInfo
            //            {
            //                Id = 0,
            //                Type = NodeType.Column
            //            }
            //        }).ToArray())
            //        {
            //            Tag = new NodeInfo
            //            {
            //                Id = 0,
            //                Type = NodeType.Table
            //            }
            //        }
            //    ).ToArray());


            return treeNodes;
        }

        public async Task<DataSet> DataSet(string query, string text)
        {
            return await this.db.DataSet(query, text);
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
