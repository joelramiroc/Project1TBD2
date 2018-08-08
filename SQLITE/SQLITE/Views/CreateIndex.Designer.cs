namespace SQLITE.Views
{
    partial class CreateIndex
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
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.indexName = new System.Windows.Forms.TextBox();
            this.ddl = new System.Windows.Forms.RichTextBox();
            this.tables = new System.Windows.Forms.ComboBox();
            this.columns = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(196, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(328, 52);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(81, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "IS UNIQUE";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name Index";
            // 
            // indexName
            // 
            this.indexName.Location = new System.Drawing.Point(127, 49);
            this.indexName.Name = "indexName";
            this.indexName.Size = new System.Drawing.Size(100, 20);
            this.indexName.TabIndex = 5;
            // 
            // ddl
            // 
            this.ddl.Location = new System.Drawing.Point(12, 188);
            this.ddl.Name = "ddl";
            this.ddl.Size = new System.Drawing.Size(462, 148);
            this.ddl.TabIndex = 10;
            this.ddl.Text = "";
            // 
            // tables
            // 
            this.tables.FormattingEnabled = true;
            this.tables.Location = new System.Drawing.Point(60, 92);
            this.tables.Name = "tables";
            this.tables.Size = new System.Drawing.Size(121, 21);
            this.tables.TabIndex = 13;
            this.tables.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // columns
            // 
            this.columns.FormattingEnabled = true;
            this.columns.Location = new System.Drawing.Point(229, 92);
            this.columns.Name = "columns";
            this.columns.Size = new System.Drawing.Size(121, 21);
            this.columns.TabIndex = 14;
            // 
            // CreateIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 348);
            this.Controls.Add(this.columns);
            this.Controls.Add(this.tables);
            this.Controls.Add(this.ddl);
            this.Controls.Add(this.indexName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Name = "CreateIndex";
            this.Text = "CreateIndex";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox indexName;
        private System.Windows.Forms.RichTextBox ddl;
        private System.Windows.Forms.ComboBox tables;
        private System.Windows.Forms.ComboBox columns;
    }
}