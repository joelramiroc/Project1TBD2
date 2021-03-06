﻿using SQLITE.Controller;
using SQLITE.Models;
using SQLITE.Services;
using SQLITE.Views;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLITE
{
    public partial class PrincipalLayout : Form
    {

        PrincipalLayoutController controller;
        bool isConnect;
        TableCreate TableCreate;
        CreateView CreateView;
        CreateTrigger CreateTrigger;
        RegisterOfTableView RegisterOfTableView;
        CreateIndex CreateIndex;
        AlterTable AlterTable;
        CreateDatabase createDatabase;
        AlterTrigger AlterTrigger;
        CreateForeignKey CreateForeignKey;
        public PrincipalLayout()
        {
            InitializeComponent();
            this.isConnect = false;
            this.controller = new PrincipalLayoutController();
            this.treeViewDataConecction.NodeMouseClick += TreeViewDataConecction_NodeMouseClick;
            this.treeViewDataConecction.MouseDoubleClick += TreeViewDataConecction_MouseDoubleClick;
            this.controller = new PrincipalLayoutController();
        }

        private async void TreeViewDataConecction_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var node = this.treeViewDataConecction.SelectedNode;
            if (node == null)
            {
                return;
            }
            var nodeInfo = (NodeInfo)node.Tag;
            if (nodeInfo == null)
            {
                return;
            }
            else if (nodeInfo.Type == NodeType.Table)
            {
                string query = $"pragma table_info('{node.Text}')";
                await this.Consulta(query, node.Text);
            }
            else if (nodeInfo.Type == NodeType.View)
            {
                string query = $"select * from {node.Text}";
                await this.Consulta(query, node.Text);
            }
            else if (nodeInfo.Type == NodeType.Trigger)
            {
                string query = $"select sql from sqlite_master where name = '{node.Text}' and type = 'trigger'";
                var result = await this.controller.Consulta(query);
                var c = "";

                while (result.Read())
                {
                    c += result["sql"].ToString();

                }

                this.manualQuerys.Text = c;
            }

            return;
        }

        public async Task Consulta(string query, string title)
        {
            var dataSet = await this.controller.DataSet(query, title);
            this.dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void TreeViewDataConecction_NodeMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = this.treeViewDataConecction.GetNodeAt(new Point(e.X, e.Y));
                var nodeInfo = (NodeInfo)node.Tag;

                if (nodeInfo.Type == NodeType.Menu)
                {
                    this.menuMenus.Tag = node;
                    this.menuMenus.Show(Cursor.Position);
                }
                else if (nodeInfo.Type == NodeType.Table || nodeInfo.Type == NodeType.View || nodeInfo.Type == NodeType.Trigger || nodeInfo.Type == NodeType.Index)
                {
                    this.menuElements.Tag = node;
                    this.menuElements.Show(Cursor.Position);
                }

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
                        var nodes = (await this.controller.GethTreeNodes()).ToArray();
                        this.treeViewDataConecction.Nodes.AddRange(nodes);
                        this.isConnect = true;
                        this.createD.Visible = false;
                        this.pictureBox3.Visible = true;
                        this.pictureBox2.BackgroundImage = Image.FromFile(@"..\..\Resources\cancel.png");
                    }
                    else
                    {
                        MessageBox.Show("Cannot open the database selected");
                        return;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Cannot open the file selected");
                    return;
                }

            }
            else
            {
                await this.controller.CloseDataBase();
                this.isConnect = false;
                this.pictureBox3.Visible = false;
                this.createD.Visible = true;
                this.pictureBox2.BackgroundImage = Image.FromFile(@"..\..\Resources\open.png");
                this.treeViewDataConecction.Nodes.Clear();
                dataGridView1.DataSource = null;
            }
        }

        public async Task<bool> ConnectWithDatabaseAsync()
        {
            string path = string.Empty;
            openFileDialog1.Filter = "Databases SQLite (*.db, *.sqlite, *.bd, *.data, *.s3db)|*.db;*.sqlite;*.bd;*.data *.s3db";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
            }

            return await this.controller.OpenDataBase(path);
        }

        private async void pictureBox1_Click(object sender, EventArgs e)
{
    if (this.isConnect)
    {
        if (this.manualQuerys.TextLength == 0)
            {
                MessageBox.Show("The query is empty");
                return;
            }
            var text = this.manualQuerys.Text;
            string command = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text.ElementAt(i).Equals(';'))
                {
                    break;
                }
                else
                {
                    command += text.ElementAt(i);
                }
            }
            try
            {
                await this.Consulta(command, "QUERY");
            }
            catch (Exception ea)
            {
                try
                {
                    var result = await this.controller.ExecuteConsult("QUERY");
                    this.manualQuerys.Text += result.ToString();
                }
                catch (Exception ss)
                {
                    MessageBox.Show("Error in command:" + ss.Message);
                }

                }
            }
            else
            {
                MessageBox.Show("You need connect to a database.");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void treeViewDataConecction_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeViewDataConecction_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if ((e.Node.Tag as NodeInfo)?.Type == NodeType.Menu)
            {
                e.Node.ImageKey = "carpeta abierta";
                e.Node.SelectedImageKey = "carpeta abierta";
                base.Invalidate(e.Node.Bounds);
            }
        }

        private void treeViewDataConecction_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if ((e.Node.Tag as NodeInfo)?.Type == NodeType.Menu)
            {
                e.Node.ImageKey = "carpeta cerrada";
                e.Node.SelectedImageKey = "carpeta cerrada";
                base.Invalidate(e.Node.Bounds);
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private async void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = (TreeNode)this.menuElements.Tag;
            if (node == null)
            {
                return;
            }
            var parent = (TreeNode)node.Parent;
            if (parent == null)
            {
                return;
            }
            if (parent.Text.Equals("TABLES"))
            {
                var table = this.controller.datab.Tables.Find(x => x.TableName.Equals(node.Text));
                if (table == null)
                {
                    return;
                }
                this.AlterTable = new AlterTable(table, await this.controller.GetSQliteConecction());
                this.AlterTable.ShowDialog();
            }
            else if (parent.Text.Equals("VIEWS"))
            {
                string querys = $"select * from sqlite_master where name = '{node.Text}' and type = 'view'";
                var results = await this.controller.Consulta(querys);
                var c = $"drop view {node.Text} ; \n \n";

                while (results.Read())
                {
                    c += results["sql"].ToString();

                }
                this.CreateView = new CreateView(false, c);
                if (this.CreateView.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var query = this.CreateView.Sql;
                        var result = await this.controller.ExecuteConsult(query);
                        if (result == 0)
                        {
                            this.treeViewDataConecction.Nodes.Clear();
                            var nodes = (await this.controller.GethTreeNodes()).ToArray();
                            this.treeViewDataConecction.Nodes.AddRange(nodes);
                            MessageBox.Show("The view was edited suscefully");
                        }
                        else
                        {
                            MessageBox.Show("Not was edited the view");
                        }
                    }
                    catch (Exception ews)
                    {
                        MessageBox.Show("Internal error: \n" + ews.Message);
                    }

                }
            }
            else if (parent.Text.Equals("TRIGGERS"))
            {

                string querys = $"select * from sqlite_master where name = '{node.Text}' and type = 'trigger'";
                var results = await this.controller.Consulta(querys);
                var c = "";
                var drop = $"drop trigger {node.Text} ; \n \n";

                while (results.Read())
                {
                    c += results["sql"].ToString();

                }
                this.manualQuerys.Text = c;
                this.AlterTrigger = new AlterTrigger(c);
                if (this.AlterTrigger.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var query = this.AlterTrigger.Sql;
                        await this.controller.ExecuteConsult(drop);
                        var result = await this.controller.ExecuteConsult(query);
                        if (result == 0)
                        {
                            this.treeViewDataConecction.Nodes.Clear();
                            var nodes = (await this.controller.GethTreeNodes()).ToArray();
                            this.treeViewDataConecction.Nodes.AddRange(nodes);
                            MessageBox.Show("The trigger was edited suscefully");
                        }
                        else
                        {
                            MessageBox.Show("Not was edited the view");
                        }
                    }
                    catch (Exception ews)
                    {
                        MessageBox.Show("Internal error: \n" + ews.Message);
                    }

                }


            }
            else
            {

            }

        }

        private void secondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //add
        }

        private async void deleteToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                var node = (TreeNode)this.menuElements.Tag;
                if (node == null)
                {
                    MessageBox.Show("Anny internal error");
                    return;
                }
                else
                {
                    var nodeInfo = (NodeInfo)node.Tag;
                    if (nodeInfo == null)
                    {
                        MessageBox.Show("Anny internal error");
                        return;
                    }
                    else if (nodeInfo.Type == NodeType.Table)
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        string caption = "Advertence";
                        DialogResult result;
                        result = MessageBox.Show("Are you sure", caption, buttons);
                        if (result == DialogResult.Yes)
                        {
                            string query = $"drop table {node.Text}";
                            var resultd = await this.controller.ExecuteConsult(query);
                            if (resultd == 0)
                            {
                                node.Remove();
                                MessageBox.Show("The table was drop succefully");
                            }
                            else
                            {
                                MessageBox.Show("Any internal error o register in other table depends of this table");
                            }
                        }
                    }
                    else if (nodeInfo.Type == NodeType.View)
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        string caption = "Advertence";
                        DialogResult result;
                        result = MessageBox.Show("Are you sure", caption, buttons);
                        if (result == DialogResult.Yes)
                        {
                            string query = $"drop view {node.Text}";
                            var resultd = await this.controller.ExecuteConsult(query);
                            if (resultd == 0)
                            {
                                node.Remove();
                                MessageBox.Show("The view was drop succefully");
                            }
                            else
                            {
                                MessageBox.Show("Any internal error.");
                            }
                        }
                    }
                    else if (nodeInfo.Type == NodeType.Index)
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        string caption = "Advertence";
                        DialogResult result;
                        result = MessageBox.Show("Are you sure", caption, buttons);
                        if (result == DialogResult.Yes)
                        {
                            string query = $"drop index {node.Text}";
                            var resultd = await this.controller.ExecuteConsult(query);
                            if (resultd == 0)
                            {
                                node.Remove();
                                MessageBox.Show("The index was drop succefully");
                            }
                            else
                            {
                                MessageBox.Show("Any internal error.");
                            }
                        }
                    }
                    else if (nodeInfo.Type == NodeType.Trigger)
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        string caption = "Advertence";
                        DialogResult result;
                        result = MessageBox.Show("Are you sure", caption, buttons);
                        if (result == DialogResult.Yes)
                        {
                            string query = $"drop trigger {node.Text}";
                            var resultd = await this.controller.ExecuteConsult(query);
                            if (resultd == 0)
                            {
                                node.Remove();
                                MessageBox.Show("The trigger was drop succefully");
                            }
                            else
                            {
                                MessageBox.Show("Any internal error.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Anny internal error");
                        return;
                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Anny internal error");
            }

        }

        private void contextMenuStrip1_Opening_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private async void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var node = (TreeNode)this.menuMenus.Tag;
            if (node == null)
            {
                return;
            }
            if (node.Text.Equals("TABLES"))
            {
                this.TableCreate = new TableCreate(true, string.Empty);

                if (TableCreate.ShowDialog() == DialogResult.OK)
                {
                    try
                    {

                        var result = await this.controller.ExecuteConsult(TableCreate.Query);
                        if (result == 0)
                        {
                            this.treeViewDataConecction.Nodes.Clear();
                            var nodes = (await this.controller.GethTreeNodes()).ToArray();
                            this.treeViewDataConecction.Nodes.AddRange(nodes);
                            MessageBox.Show("The table was crate suscefully");
                        }
                        else
                        {
                            MessageBox.Show("Not was create the table");
                        }
                    }
                    catch (Exception exc)
                    {

                        MessageBox.Show("Internal error:" + exc.Message);
                    }
                }
            }
            else if (node.Text.Equals("VIEWS"))
            {
                this.CreateView = new CreateView(true, string.Empty);
                if (this.CreateView.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var query = this.CreateView.Sql;
                        var result = await this.controller.ExecuteConsult(query);
                        if (result == 0)
                        {
                            this.treeViewDataConecction.Nodes.Clear();
                            var nodes = (await this.controller.GethTreeNodes()).ToArray();
                            this.treeViewDataConecction.Nodes.AddRange(nodes);
                            MessageBox.Show("The view was crate suscefully");
                        }
                        else
                        {
                            MessageBox.Show("Not was create the view");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Internal error");
                    }

                }
            }
            else if (node.Text.Equals("TRIGGERS"))
            {
                this.CreateTrigger = new CreateTrigger(true, string.Empty,this.controller.datab);
                if (this.CreateTrigger.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var query = this.CreateTrigger.Sql;
                        var result = await this.controller.ExecuteConsult(query);
                        if (result == 0)
                        {
                            this.treeViewDataConecction.Nodes.Clear();
                            var nodes = (await this.controller.GethTreeNodes()).ToArray();
                            this.treeViewDataConecction.Nodes.AddRange(nodes);
                            MessageBox.Show("The trigger was crate suscefully");
                        }
                        else
                        {
                            MessageBox.Show("Not was create the trigger");
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Internal error:" + exc.Message);
                    }
                }
            }
            else if (node.Text.Equals("INDEXS"))
            {
                var nodeP = node.Parent;
                if (nodeP == null)
                {
                    return;
                }
                this.CreateIndex = new CreateIndex(true, "", this.controller.datab,nodeP.Text);
                if (this.CreateIndex.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var query = this.CreateIndex.Sql;
                        var result = await this.controller.ExecuteConsult(query);
                        if (result == 0)
                        {
                            this.treeViewDataConecction.Nodes.Clear();
                            var nodes = (await this.controller.GethTreeNodes()).ToArray();
                            this.treeViewDataConecction.Nodes.AddRange(nodes);
                            MessageBox.Show("The index was crate suscefully");
                        }
                        else
                        {
                            MessageBox.Show("Not was create the index");
                        }
                    }
                    catch (Exception esx)
                    {
                        MessageBox.Show("Internal error:" + esx.Message);
                    }
                }

            }
        }

        private async void viewInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var node = (TreeNode)this.menuElements.Tag;
            if (node == null)
            {
                MessageBox.Show("Anny internal error");
                return;
            }
            var nodeInfo = (NodeInfo)node.Tag;
            if (nodeInfo == null)
            {
                MessageBox.Show("Anny internal error");
                return;
            }
            if (nodeInfo.Type == NodeType.Table)
            {
                try
                {
                    string query = $"select * from {node.Text}";
                    var dataSet = await this.controller.DataSet(query, node.Text);
                    var table = this.controller.datab.Tables.Find(x => x.TableName.Equals(node.Text));
                    this.RegisterOfTableView = new RegisterOfTableView(node.Text, dataSet,table, await this.controller.GetSQliteConecction());
                    if (this.RegisterOfTableView.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("Cool");
                    }
                    else
                    {
                        MessageBox.Show("Anny internal error");
                    }

                }
                catch (Exception exc)
                {

                    MessageBox.Show("Internal error:" + exc.Message);
                }
            }
        }

        private async void createD_Click(object sender, EventArgs e)
        {
            this.createDatabase = new CreateDatabase();
            if (this.createDatabase.ShowDialog() == DialogResult.OK)
            {
                var result = await this.controller.CreaateDatabase(this.createDatabase.DatabasePath,this.createDatabase.DatabaseName);
                if (result)
                {
                    MessageBox.Show("Database created");
                }
                else
                {
                    MessageBox.Show("Cant create the database");
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private async void addForeignKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var node = this.treeViewDataConecction.SelectedNode;
            if (node == null)
            {
                return;
            }
            var nodeInfo = (NodeInfo)node.Tag;
            if (nodeInfo == null)
            {
                return;
            }
            if (nodeInfo.Type == NodeType.Table)
            {
                this.CreateForeignKey = new CreateForeignKey(this.controller.datab,node.Text, await this.controller.GetSQliteConecction());
                this.CreateForeignKey.ShowDialog();
            }
        }

        private async void pictureBox3_Click(object sender, EventArgs e)
        {
            if(this.isConnect)
            {

                try
                {
                    await this.controller.CloseDataBase();
                    this.isConnect = false;
                    this.createD.Visible = true;
                    this.pictureBox2.BackgroundImage = Image.FromFile(@"..\..\Resources\open.png");
                    this.treeViewDataConecction.Nodes.Clear();
                    dataGridView1.DataSource = null;
                    var result = await this.controller.DeleteDataBase();
                    MessageBox.Show("Database dropped");
                }
                catch(Exception es)
                {
                    MessageBox.Show("Any error:" + es.Message);
                }
            }
        }
    }
}