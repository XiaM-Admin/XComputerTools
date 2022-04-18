namespace My_Computer_Tools_Ⅱ
{
    partial class Form_input
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
            this.tbox_input = new System.Windows.Forms.TextBox();
            this.lab_Tip = new System.Windows.Forms.Label();
            this.but_Done = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbox_input
            // 
            this.tbox_input.Location = new System.Drawing.Point(12, 36);
            this.tbox_input.Name = "tbox_input";
            this.tbox_input.Size = new System.Drawing.Size(193, 21);
            this.tbox_input.TabIndex = 0;
            this.tbox_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tbox_input_KeyPress);
            // 
            // lab_Tip
            // 
            this.lab_Tip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_Tip.AutoSize = true;
            this.lab_Tip.Location = new System.Drawing.Point(12, 14);
            this.lab_Tip.Name = "lab_Tip";
            this.lab_Tip.Size = new System.Drawing.Size(41, 12);
            this.lab_Tip.TabIndex = 1;
            this.lab_Tip.Text = "label1";
            this.lab_Tip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // but_Done
            // 
            this.but_Done.Location = new System.Drawing.Point(130, 65);
            this.but_Done.Name = "but_Done";
            this.but_Done.Size = new System.Drawing.Size(75, 23);
            this.but_Done.TabIndex = 2;
            this.but_Done.Text = "完成";
            this.but_Done.UseVisualStyleBackColor = true;
            this.but_Done.Click += new System.EventHandler(this.But_Done_Click);
            // 
            // Form_input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 98);
            this.Controls.Add(this.but_Done);
            this.Controls.Add(this.lab_Tip);
            this.Controls.Add(this.tbox_input);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form_input";
            this.Text = "输入";
            this.Load += new System.EventHandler(this.Form_input_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lab_Tip;
        private System.Windows.Forms.Button but_Done;
        public System.Windows.Forms.TextBox tbox_input;
    }
}