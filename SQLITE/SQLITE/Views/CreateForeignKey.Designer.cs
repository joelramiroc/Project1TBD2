namespace SQLITE.Views
{
    partial class CreateForeignKey
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
            this.columns = new System.Windows.Forms.ComboBox();
            this.ColumnsDestino = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tablesDestino = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ddl = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // columns
            // 
            this.columns.FormattingEnabled = true;
            this.columns.Location = new System.Drawing.Point(116, 56);
            this.columns.Name = "columns";
            this.columns.Size = new System.Drawing.Size(121, 21);
            this.columns.TabIndex = 15;
            // 
            // ColumnsDestino
            // 
            this.ColumnsDestino.FormattingEnabled = true;
            this.ColumnsDestino.Location = new System.Drawing.Point(116, 199);
            this.ColumnsDestino.Name = "ColumnsDestino";
            this.ColumnsDestino.Size = new System.Drawing.Size(121, 21);
            this.ColumnsDestino.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Column";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Column";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Table Reference";
            // 
            // tablesDestino
            // 
            this.tablesDestino.FormattingEnabled = true;
            this.tablesDestino.Location = new System.Drawing.Point(171, 125);
            this.tablesDestino.Name = "tablesDestino";
            this.tablesDestino.Size = new System.Drawing.Size(121, 21);
            this.tablesDestino.TabIndex = 19;
            this.tablesDestino.SelectedIndexChanged += new System.EventHandler(this.tablesDestino_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(397, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ddl
            // 
            this.ddl.Location = new System.Drawing.Point(44, 226);
            this.ddl.Name = "ddl";
            this.ddl.Size = new System.Drawing.Size(462, 129);
            this.ddl.TabIndex = 22;
            this.ddl.Text = "";
            // 
            // CreateForeignKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 379);
            this.Controls.Add(this.ddl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tablesDestino);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ColumnsDestino);
            this.Controls.Add(this.columns);
            this.Name = "CreateForeignKey";
            this.Text = "CreateForeignKey";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox columns;
        private System.Windows.Forms.ComboBox ColumnsDestino;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox tablesDestino;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox ddl;
    }
}