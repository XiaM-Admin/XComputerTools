using System;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public partial class Form_input : Form
    {
        public Form_input(string tip)
        {
            InitializeComponent();
            lab_Tip.Text = tip;
        }

        private void Form_input_Load(object sender, EventArgs e)
        {
            tbox_input.Text = "";
        }

        private void but_Done_Click(object sender, EventArgs e)
        {
            if (tbox_input.Text == "")
            {
                MessageBox.Show("请输入内容！");
                return;
            }
            this.Hide();
            this.Close();
        }

        private void tbox_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                but_Done.PerformClick();
        }
    }
}
