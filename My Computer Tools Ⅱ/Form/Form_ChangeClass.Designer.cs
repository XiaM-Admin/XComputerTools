namespace My_Computer_Tools_Ⅱ
{
    partial class Form_ChangeClass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ChangeClass));
            this.Lbox_Class = new System.Windows.Forms.ListBox();
            this.but_Done = new System.Windows.Forms.Button();
            this.but_Add = new System.Windows.Forms.Button();
            this.but_Up = new System.Windows.Forms.Button();
            this.but_Down = new System.Windows.Forms.Button();
            this.but_Del = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Lbox_Class
            // 
            this.Lbox_Class.FormattingEnabled = true;
            this.Lbox_Class.ItemHeight = 12;
            this.Lbox_Class.Location = new System.Drawing.Point(12, 12);
            this.Lbox_Class.Name = "Lbox_Class";
            this.Lbox_Class.Size = new System.Drawing.Size(297, 256);
            this.Lbox_Class.TabIndex = 0;
            // 
            // but_Done
            // 
            this.but_Done.Location = new System.Drawing.Point(315, 233);
            this.but_Done.Name = "but_Done";
            this.but_Done.Size = new System.Drawing.Size(98, 32);
            this.but_Done.TabIndex = 1;
            this.but_Done.Text = "完成";
            this.but_Done.UseVisualStyleBackColor = true;
            this.but_Done.Click += new System.EventHandler(this.But_Done_Click);
            // 
            // but_Add
            // 
            this.but_Add.Location = new System.Drawing.Point(315, 12);
            this.but_Add.Name = "but_Add";
            this.but_Add.Size = new System.Drawing.Size(98, 32);
            this.but_Add.TabIndex = 2;
            this.but_Add.Text = "添加|Add";
            this.but_Add.UseVisualStyleBackColor = true;
            this.but_Add.Click += new System.EventHandler(this.But_Add_Click);
            // 
            // but_Up
            // 
            this.but_Up.Location = new System.Drawing.Point(315, 50);
            this.but_Up.Name = "but_Up";
            this.but_Up.Size = new System.Drawing.Size(98, 32);
            this.but_Up.TabIndex = 3;
            this.but_Up.Text = "向上|Up";
            this.but_Up.UseVisualStyleBackColor = true;
            this.but_Up.Click += new System.EventHandler(this.But_Up_Click);
            // 
            // but_Down
            // 
            this.but_Down.Location = new System.Drawing.Point(315, 88);
            this.but_Down.Name = "but_Down";
            this.but_Down.Size = new System.Drawing.Size(98, 32);
            this.but_Down.TabIndex = 4;
            this.but_Down.Text = "向下|Down";
            this.but_Down.UseVisualStyleBackColor = true;
            this.but_Down.Click += new System.EventHandler(this.But_Down_Click);
            // 
            // but_Del
            // 
            this.but_Del.Location = new System.Drawing.Point(315, 126);
            this.but_Del.Name = "but_Del";
            this.but_Del.Size = new System.Drawing.Size(98, 32);
            this.but_Del.TabIndex = 5;
            this.but_Del.Text = "删除|Del";
            this.but_Del.UseVisualStyleBackColor = true;
            this.but_Del.Click += new System.EventHandler(this.But_Del_Click);
            // 
            // Form_ChangeClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 278);
            this.Controls.Add(this.but_Del);
            this.Controls.Add(this.but_Down);
            this.Controls.Add(this.but_Up);
            this.Controls.Add(this.but_Add);
            this.Controls.Add(this.but_Done);
            this.Controls.Add(this.Lbox_Class);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_ChangeClass";
            this.Text = "修改Class";
            this.Load += new System.EventHandler(this.Form_ChangeClass_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox Lbox_Class;
        private System.Windows.Forms.Button but_Done;
        private System.Windows.Forms.Button but_Add;
        private System.Windows.Forms.Button but_Up;
        private System.Windows.Forms.Button but_Down;
        private System.Windows.Forms.Button but_Del;
    }
}