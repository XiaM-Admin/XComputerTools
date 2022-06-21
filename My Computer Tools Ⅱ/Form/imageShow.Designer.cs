namespace My_Computer_Tools_Ⅱ
{
    partial class imageShow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(imageShow));
            this.picbox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BBCodetxt = new System.Windows.Forms.TextBox();
            this.HTMLtxt = new System.Windows.Forms.TextBox();
            this.MDtxt = new System.Windows.Forms.TextBox();
            this.imgurltxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listBox = new System.Windows.Forms.ListBox();
            this.lab_tip = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picbox)).BeginInit();
            this.SuspendLayout();
            // 
            // picbox
            // 
            this.picbox.ImageLocation = "";
            this.picbox.Location = new System.Drawing.Point(393, 13);
            this.picbox.Margin = new System.Windows.Forms.Padding(4);
            this.picbox.Name = "picbox";
            this.picbox.Size = new System.Drawing.Size(326, 274);
            this.picbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbox.TabIndex = 1;
            this.picbox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(406, 355);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "BBCode：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(419, 394);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "HTML：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(390, 433);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Markdown：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(389, 472);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Image URL：";
            // 
            // BBCodetxt
            // 
            this.BBCodetxt.Location = new System.Drawing.Point(480, 352);
            this.BBCodetxt.Name = "BBCodetxt";
            this.BBCodetxt.ReadOnly = true;
            this.BBCodetxt.Size = new System.Drawing.Size(239, 23);
            this.BBCodetxt.TabIndex = 6;
            this.BBCodetxt.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.BBCodetxt_MouseDoubleClick);
            // 
            // HTMLtxt
            // 
            this.HTMLtxt.Location = new System.Drawing.Point(480, 391);
            this.HTMLtxt.Name = "HTMLtxt";
            this.HTMLtxt.ReadOnly = true;
            this.HTMLtxt.Size = new System.Drawing.Size(239, 23);
            this.HTMLtxt.TabIndex = 7;
            this.HTMLtxt.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.BBCodetxt_MouseDoubleClick);
            // 
            // MDtxt
            // 
            this.MDtxt.Location = new System.Drawing.Point(480, 430);
            this.MDtxt.Name = "MDtxt";
            this.MDtxt.ReadOnly = true;
            this.MDtxt.Size = new System.Drawing.Size(239, 23);
            this.MDtxt.TabIndex = 8;
            this.MDtxt.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.BBCodetxt_MouseDoubleClick);
            // 
            // imgurltxt
            // 
            this.imgurltxt.Location = new System.Drawing.Point(480, 469);
            this.imgurltxt.Name = "imgurltxt";
            this.imgurltxt.ReadOnly = true;
            this.imgurltxt.Size = new System.Drawing.Size(239, 23);
            this.imgurltxt.TabIndex = 9;
            this.imgurltxt.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.BBCodetxt_MouseDoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(393, 293);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(326, 51);
            this.label5.TabIndex = 10;
            this.label5.Text = "如果图片为空或未刷新，请去主界面点击\'刷新列表\'按钮后，\r\n重新进入即可。\r\n双击编辑框复制相应值！";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 17;
            this.listBox.Location = new System.Drawing.Point(12, 12);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(370, 480);
            this.listBox.TabIndex = 11;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // lab_tip
            // 
            this.lab_tip.AutoSize = true;
            this.lab_tip.Location = new System.Drawing.Point(12, 504);
            this.lab_tip.Name = "lab_tip";
            this.lab_tip.Size = new System.Drawing.Size(38, 17);
            this.lab_tip.TabIndex = 12;
            this.lab_tip.Text = "TIP：";
            // 
            // imageShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 532);
            this.Controls.Add(this.lab_tip);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.imgurltxt);
            this.Controls.Add(this.MDtxt);
            this.Controls.Add(this.HTMLtxt);
            this.Controls.Add(this.BBCodetxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picbox);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "imageShow";
            this.Text = "imageShow";
            this.Load += new System.EventHandler(this.imageShow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox BBCodetxt;
        private System.Windows.Forms.TextBox HTMLtxt;
        private System.Windows.Forms.TextBox MDtxt;
        private System.Windows.Forms.TextBox imgurltxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Label lab_tip;
    }
}