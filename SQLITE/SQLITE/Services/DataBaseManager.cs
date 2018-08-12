// <copyright file="DataBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SQLite.Services
{
    using SQLITE.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using System.Threading.Tasks;

    public class DataBaseConnection
    {
        public SQLiteConnection SQLiteConnection;
        public string DatabaseName { get; set; }

        public string Databasepath { get; set; }


        public async Task<bool> CreateDataBase(string path, string databaseName)
        {
            this.DatabaseName = databaseName;
            this.Databasepath = path;
            var complete = $@"{path}\{databaseName}.db";
            this.SQLiteConnection = new SQLiteConnection($@"Data Source = {complete}");

            if (!File.Exists(complete))
            {
                SQLiteConnection.CreateFile(complete);
                return true;
            }

            return false;
        }

        public async Task<bool> OpenConecttionDatabase()
        {
            if (this.SQLiteConnection.State != ConnectionState.Open)
            {
                this.SQLiteConnection.Open();
                return true;
            }

            return false;
        }

        public async Task<SQLiteDataReader> ConsultaLectura(string query)
        {
            SQLiteCommand sQLiteCommand = new SQLiteCommand(query, this.SQLiteConnection);
            return sQLiteCommand.ExecuteReader();
        }

        public async Task<List<TableModel>> GetTablesDataBase()
        {
            List<TableModel> TableModels = new List<TableModel>();
            string queryTable = @"select * FROM sqlite_master WHERE type ='table'";
            var tableResult = await this.ConsultaLectura(queryTable);

            while (tableResult.Read())
            {
                var tableLeido = tableResult["name"];
                var tableName = tableLeido.ToString();

                string queryColumns = $@"PRAGMA table_info({tableName})";
                var ColumResult = await this.ConsultaLectura(queryColumns);

                var tablemodel = new TableModel
                {
                    TableName = tableName,
                    Columns = new List<ColumnModel>()
                };

                while (ColumResult.Read())
                {
                    try
                    {
                        var ColumnLeido = ColumResult["name"].ToString();
                        var Columntype = ColumResult["type"].ToString();
                        var Columnnotnull = Convert.ToBoolean(ColumResult["notnull"]);
                        var ColumntsfltValue = ColumResult["dflt_value"].ToString();
                        var columnscid = Convert.ToInt32(ColumResult["cid"]);
                        var Columntspk = Convert.ToBoolean(ColumResult["pk"]);
                        tablemodel.Columns.Add(new ColumnModel
                        {
                            ColumnName = ColumnLeido.ToString(),
                            DateType = Columntype,
                            Cid = columnscid,
                            DefaultValue = ColumntsfltValue,
                            IsNull = Columnnotnull,
                            IsPrimaryKey = Columntspk
                        });

                    }
                    catch (System.Exception asd)
                    {
                        var s = asd.Message;
                    }
                }

                TableModels.Add(tablemodel);
            }

            return TableModels;
        }

        public async Task<List<ViewModel>> GetViewsDataBase()
        {
            var views = new List<ViewModel>();
            string query = @"select * FROM sqlite_master WHERE type ='view'";
            SQLiteCommand sQLiteCommand3 = new SQLiteCommand(query, this.SQLiteConnection);
            var result = sQLiteCommand3.ExecuteReader();
            while (result.Read())
            {
                views.Add(new ViewModel
                {
                    Id = 0,
                    ViewName = result["name"].ToString(),
                    Sql = result["sql"].ToString(),
                });
            }

            return views;
        }

        public async Task<List<CheckModel>> GetChecksDataBase()
        {
            var hecks = new List<CheckModel>();
            string query = @"PRAGMA foreign_key_check";
            SQLiteCommand sQLiteCommand3 = new SQLiteCommand(query, this.SQLiteConnection);
            var result = sQLiteCommand3.ExecuteReader();
            while (result.Read())
            {
                hecks.Add(new CheckModel
                {
                    Id = 0,
                    CheckName = result["name"].ToString()
                });
            }

            return hecks;
        }

        public async Task<List<TriggerModel>> GetTriggersDataBase()
        {
            var triggers = new List<TriggerModel>();
            string query = @"select * FROM sqlite_master WHERE type ='trigger'";
            SQLiteCommand sQLiteCommand3 = new SQLiteCommand(query, this.SQLiteConnection);
            var result = sQLiteCommand3.ExecuteReader();
            while (result.Read())
            {
                triggers.Add(new TriggerModel
                {
                    Id = 0,
                    TriggerName = result["name"].ToString(),
                    Sql = result["sql"].ToString(),
                });
            }

            return triggers;
        }

        public async Task<List<IndexModel>> GetIndexTable(string tableName)
        {
            var indexs = new List<IndexModel>();
            string query = $@"select * from sqlite_master where type='index' and tbl_name = '{ tableName}'";
            SQLiteCommand sQLiteCommand3 = new SQLiteCommand(query, this.SQLiteConnection);
            var result = sQLiteCommand3.ExecuteReader();
            while (result.Read())
            {
                indexs.Add(new IndexModel
                {
                    Id = 0,
                    IndexNamenName = result["name"].ToString(),
                    Sql = result["sql"].ToString(),
                });
            }

            return indexs;
        }

        public async Task<int> ExecuteQuery(string query)
        {
            try
            {
                SQLiteCommand sQLiteCommand = new SQLiteCommand(query, this.SQLiteConnection);
                return sQLiteCommand.ExecuteNonQuery();
            }
            catch (Exception s)
            {
                var ss = s.Message;
                return 1;
            }
        }

        public async Task<bool> OpenDataBase(string path)
        {
            this.Databasepath = path;
            this.SQLiteConnection = new SQLiteConnection($@"Data Source = {path}");

            if (!File.Exists(path))
            {
                return false;
            }

            return await this.OpenConecttionDatabase();
        }

        public async Task<bool> CloseDatabase()
        {
            if (this.SQLiteConnection.State != ConnectionState.Closed)
            {
                this.SQLiteConnection.Clone();
                this.SQLiteConnection.Close();
                return true;
            }

            return false;
        }

        public async Task<DataSet> DataSet(string query, string text)
        {
            var sQLiteDataAdapter = new SQLiteDataAdapter(query, this.SQLiteConnection);
            DataSet dataSet = new DataSet();
            sQLiteDataAdapter.Fill(dataSet, text);
            return dataSet;
        }
    }
}