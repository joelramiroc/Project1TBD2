namespace SQLITE.Views
{
    partial class CreateTrigger
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
            this.ddl = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.triggerName = new System.Windows.Forms.TextBox();
            this.combWhen = new System.Windows.Forms.ComboBox();
            this.comboAction = new System.Windows.Forms.ComboBox();
            this.comboTable = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.texboxCondition = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(259, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ddl
            // 
            this.ddl.Location = new System.Drawing.Point(58, 140);
            this.ddl.Name = "ddl";
            this.ddl.Size = new System.Drawing.Size(462, 101);
            this.ddl.TabIndex = 11;
            this.ddl.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "CREATE TRIGGER";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // triggerName
            // 
            this.triggerName.Location = new System.Drawing.Point(130, 42);
            this.triggerName.Name = "triggerName";
            this.triggerName.Size = new System.Drawing.Size(131, 20);
            this.triggerName.TabIndex = 14;
            // 
            // combWhen
            // 
            this.combWhen.FormattingEnabled = true;
            this.combWhen.Location = new System.Drawing.Point(399, 43);
            this.combWhen.Name = "combWhen";
            this.combWhen.Size = new System.Drawing.Size(121, 21);
            this.combWhen.TabIndex = 15;
            // 
            // comboAction
            // 
            this.comboAction.FormattingEnabled = true;
            this.comboAction.Location = new System.Drawing.Point(267, 43);
            this.comboAction.Name = "comboAction";
            this.comboAction.Size = new System.Drawing.Size(121, 21);
            this.comboAction.TabIndex = 16;
            // 
            // comboTable
            // 
            this.comboTable.FormattingEnabled = true;
            this.comboTable.Location = new System.Drawing.Point(130, 68);
            this.comboTable.Name = "comboTable";
            this.comboTable.Size = new System.Drawing.Size(131, 21);
            this.comboTable.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "WHEN";
            // 
            // texboxCondition
            // 
            this.texboxCondition.Location = new System.Drawing.Point(130, 97);
            this.texboxCondition.Name = "texboxCondition";
            this.texboxCondition.Size = new System.Drawing.Size(390, 20);
            this.texboxCondition.TabIndex = 19;
            // 
            // CreateTrigger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 282);
            this.Controls.Add(this.texboxCondition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboTable);
            this.Controls.Add(this.comboAction);
            this.Controls.Add(this.combWhen);
            this.Controls.Add(this.triggerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ddl);
            this.Name = "CreateTrigger";
            this.Text = "CreateTrigger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox ddl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox triggerName;
        private System.Windows.Forms.ComboBox combWhen;
        private System.Windows.Forms.ComboBox comboAction;
        private System.Windows.Forms.ComboBox comboTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox texboxCondition;
    }
}