namespace SQLITE.Views
{
    partial class CreateView
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
            this.SuspendLayout();
            // 
            // ddl
            // 
            this.ddl.Location = new System.Drawing.Point(40, 45);
            this.ddl.Name = "ddl";
            this.ddl.Size = new System.Drawing.Size(462, 148);
            this.ddl.TabIndex = 9;
            this.ddl.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(225, 219);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CreateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 273);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ddl);
            this.Name = "CreateView";
            this.Text = "CreateView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox ddl;
        private System.Windows.Forms.Button button1;
    }
}