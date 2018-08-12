namespace SQLITE.Views
{
    partial class AlterTrigger
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
            this.manualQuerys = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // manualQuerys
            // 
            this.manualQuerys.Location = new System.Drawing.Point(57, 29);
            this.manualQuerys.Name = "manualQuerys";
            this.manualQuerys.Size = new System.Drawing.Size(627, 161);
            this.manualQuerys.TabIndex = 3;
            this.manualQuerys.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(311, 242);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Alter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AlterTrigger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 336);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.manualQuerys);
            this.Name = "AlterTrigger";
            this.Text = "AlterTrigger";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox manualQuerys;
        private System.Windows.Forms.Button button1;
    }
}