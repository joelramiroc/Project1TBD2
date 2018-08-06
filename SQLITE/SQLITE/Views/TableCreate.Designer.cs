namespace SQLITE.Views
{
    partial class TableCreate
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AutoIncrement = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ddl = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumName,
            this.ColumType,
            this.PrimaryKey,
            this.IsNull,
            this.AutoIncrement,
            this.DefaultValue});
            this.dataGridView1.Location = new System.Drawing.Point(68, 122);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(646, 150);
            this.dataGridView1.TabIndex = 4;
            // 
            // ColumName
            // 
            this.ColumName.HeaderText = "Column Name";
            this.ColumName.Name = "ColumName";
            // 
            // ColumType
            // 
            this.ColumType.HeaderText = "ColumType";
            this.ColumType.Name = "ColumType";
            // 
            // PrimaryKey
            // 
            this.PrimaryKey.HeaderText = "PrimaryKey";
            this.PrimaryKey.Name = "PrimaryKey";
            // 
            // IsNull
            // 
            this.IsNull.HeaderText = "IsNull";
            this.IsNull.Name = "IsNull";
            // 
            // AutoIncrement
            // 
            this.AutoIncrement.HeaderText = "AutoIncrement";
            this.AutoIncrement.Name = "AutoIncrement";
            // 
            // DefaultValue
            // 
            this.DefaultValue.HeaderText = "DefaultValue";
            this.DefaultValue.Name = "DefaultValue";
            // 
            // tableName
            // 
            this.tableName.Location = new System.Drawing.Point(109, 49);
            this.tableName.Name = "tableName";
            this.tableName.Size = new System.Drawing.Size(243, 20);
            this.tableName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(618, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ddl
            // 
            this.ddl.Location = new System.Drawing.Point(130, 300);
            this.ddl.Name = "ddl";
            this.ddl.Size = new System.Drawing.Size(462, 148);
            this.ddl.TabIndex = 8;
            this.ddl.Text = "";
            this.ddl.TextChanged += new System.EventHandler(this.ddl_TextChanged);
            // 
            // TableCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 489);
            this.Controls.Add(this.ddl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tableName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TableCreate";
            this.Text = "TableCreate";
            this.Load += new System.EventHandler(this.TableCreate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PrimaryKey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsNull;
        private System.Windows.Forms.TextBox tableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AutoIncrement;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultValue;
        private System.Windows.Forms.RichTextBox ddl;
    }
}