// <copyright file="DataBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SQLite.Services
{
    using System.Data.SQLite;
    using System.IO;

    public class DataBaseConnection
    {
        public SQLiteConnection SQLiteConnection;

        public DataBaseConnection()
        {
            this.SQLiteConnection = new SQLiteConnection(@"Data Source = C:\sqlite3\test.db");
            if (!File.Exists(@"C:\sqlite3\test.db"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
            }
        }

        public void OpenConecttion()
        {
            if (this.SQLiteConnection.State != System.Data.ConnectionState.Open)
            {
                this.SQLiteConnection.Open();
            }
        }

        public void CloseConecttion()
        {
            if (this.SQLiteConnection.State != System.Data.ConnectionState.Closed)
            {
                this.SQLiteConnection.Clone();
                this.SQLiteConnection.Close();
            }
        }
    }
}