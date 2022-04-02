using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public partial class Form_AccountControl : Form
    {
        private string _classname;

        public Form_AccountControl(string classname)
        {
            InitializeComponent();
            _classname = classname;
            lab_TipClass.Text += classname;
        }

        private void Form_AccountControl_Load(object sender, EventArgs e)
        {
            lbox_AccList.Items.Clear();
            //初始化账号列表

        }

        private void but_Up_Click(object sender, EventArgs e)
        {
            if (lbox_AccList.SelectedIndex == -1)
                return;
            string item = lbox_AccList.SelectedItem.ToString();
            int i = lbox_AccList.SelectedIndex;
            if (i == 0)
                return;
            string item_up = (string)lbox_AccList.Items[i - 1];
            lbox_AccList.Items[i] = item_up;
            lbox_AccList.Items[i - 1] = item;
            lbox_AccList.SelectedIndex = i - 1;
        }

        private void but_Down_Click(object sender, EventArgs e)
        {
            if (lbox_AccList.SelectedIndex == -1)
                return;
            string item = lbox_AccList.SelectedItem.ToString();
            int i = lbox_AccList.SelectedIndex;
            if (i == lbox_AccList.Items.Count - 1)
                return;
            string item_down = (string)lbox_AccList.Items[i + 1];
            lbox_AccList.Items[i] = item_down;
            lbox_AccList.Items[i + 1] = item;
            lbox_AccList.SelectedIndex = i + 1;
        }

        private void but_Del_Click(object sender, EventArgs e)
        {
            if (lbox_AccList.SelectedIndex == -1)
                return;
            int i = lbox_AccList.SelectedIndex;
            lbox_AccList.Items.Remove(lbox_AccList.SelectedItem.ToString());
            lbox_AccList.SelectedIndex = i - 1;
        }
    }
}
