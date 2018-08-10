namespace SQLITE.Views
{
    partial class AlterTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ddl = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Cid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NewType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.oldTableName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.newTableName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ddl
            // 
            this.ddl.Location = new System.Drawing.Point(141, 235);
            this.ddl.Name = "ddl";
            this.ddl.Size = new System.Drawing.Size(462, 148);
            this.ddl.TabIndex = 12;
            this.ddl.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(662, 315);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cid,
            this.ColumName,
            this.ColumType,
            this.IsNull,
            this.DefaultValue,
            this.PrimaryKey,
            this.NewType});
            this.dataGridView1.Location = new System.Drawing.Point(66, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(644, 150);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Cid
            // 
            this.Cid.HeaderText = "Cid";
            this.Cid.Name = "Cid";
            this.Cid.Visible = false;
            // 
            // ColumName
            // 
            this.ColumName.HeaderText = "Column Name";
            this.ColumName.Name = "ColumName";
            this.ColumName.ReadOnly = true;
            // 
            // ColumType
            // 
            this.ColumType.HeaderText = "ColumType";
            this.ColumType.Name = "ColumType";
            this.ColumType.ReadOnly = true;
            this.ColumType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IsNull
            // 
            this.IsNull.HeaderText = "NotNull";
            this.IsNull.Name = "IsNull";
            // 
            // DefaultValue
            // 
            this.DefaultValue.HeaderText = "DefaultValue";
            this.DefaultValue.Name = "DefaultValue";
            // 
            // PrimaryKey
            // 
            this.PrimaryKey.HeaderText = "PrimaryKey";
            this.PrimaryKey.Name = "PrimaryKey";
            // 
            // NewType
            // 
            this.NewType.HeaderText = "NewType";
            this.NewType.Name = "NewType";
            // 
            // oldTableName
            // 
            this.oldTableName.AutoSize = true;
            this.oldTableName.Location = new System.Drawing.Point(135, 25);
            this.oldTableName.Name = "oldTableName";
            this.oldTableName.Size = new System.Drawing.Size(34, 13);
            this.oldTableName.TabIndex = 13;
            this.oldTableName.Text = "Table";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Table name:";
            // 
            // newTableName
            // 
            this.newTableName.Location = new System.Drawing.Point(447, 25);
            this.newTableName.Name = "newTableName";
            this.newTableName.Size = new System.Drawing.Size(100, 20);
            this.newTableName.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(380, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "New name:";
            // 
            // AlterTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newTableName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.oldTableName);
            this.Controls.Add(this.ddl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "AlterTable";
            this.Text = "AlterTable";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ddl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsNull;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PrimaryKey;
        private System.Windows.Forms.DataGridViewComboBoxColumn NewType;
        private System.Windows.Forms.Label oldTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newTableName;
        private System.Windows.Forms.Label label2;
    }
}