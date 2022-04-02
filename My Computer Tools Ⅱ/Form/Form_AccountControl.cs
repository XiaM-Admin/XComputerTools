using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public partial class Form_AccountControl : Form
    {
        private string _classname;

        //这里使用字典 进行本地索引数据处理
        //退出时操作保存文件 
        Dictionary<string, string> AccountStrs = new Dictionary<string, string>();

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
            Console.WriteLine(_classname);

            string path = Application.StartupPath + "\\" + Program.xmlname;
            ClsXMLoperate clsXM = new ClsXMLoperate(path);
            var Vsstr = clsXM.GetNodeVsStr("UserInfo/" + _classname);
            foreach (string str in Vsstr)
            {
                lbox_AccList.Items.Add(str);//添加到显示列表
                string userAcc = clsXM.GetNodeContent("UserInfo/" + _classname + "/" + str);
                AccountStrs.Add(str, userAcc);
            }

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

        /// <summary>
        /// listbox切换选定账号时 刷新一下显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbox_AccList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Text_Username.Text = lbox_AccList.SelectedItem.ToString();
            string[] vs = AccountStrs[lbox_AccList.SelectedItem.ToString()].Split('^');
            if (vs.Length != 0)
            {
                Text_User.Text = vs[0];
                Text_UserPwd.Text = vs[1];
            }

        }
    }
}
