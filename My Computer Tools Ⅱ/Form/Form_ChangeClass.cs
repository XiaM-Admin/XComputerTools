using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public partial class Form_ChangeClass : Form
    {
        public Form_ChangeClass()
        {
            InitializeComponent();
        }

        public string retstr = "*";
        private readonly List<string> delstr = new List<string>();

        private void Form_ChangeClass_Load(object sender, EventArgs e)
        {
            //获取原始的class items
            Commands.CreatFile(Program.xmlname, true);

            ClsXMLoperate clsXM = Program.CreaterXMLHelper();
            string ret = clsXM.GetNodeContent("UserInfo/Class");
            //放入ListBox中
            Lbox_Class.Items.Clear();
            string[] vs = ret.Split('|');
            foreach (var str in vs)
            {
                Lbox_Class.Items.Add(str);
                retstr = retstr + "|" + str;
            }
            retstr = retstr.Replace("*|", "");
        }

        private void But_Add_Click(object sender, EventArgs e)
        {
            using (Form_input input = new Form_input("请输入分类组名"))
            {
                input.ShowDialog();
                string str = input.tbox_input.Text;
                if (str == "")
                    return;
                foreach (var item in Lbox_Class.Items)
                {
                    if ((string)item == str)
                    {
                        MessageBox.Show("列表中已存在 " + str + " 分类！");
                        return;
                    }
                }
                Lbox_Class.Items.Add(str);
            }
        }

        private void But_Up_Click(object sender, EventArgs e)
        {
            if (Lbox_Class.SelectedIndex == -1)
                return;
            string item = Lbox_Class.SelectedItem.ToString();
            int i = Lbox_Class.SelectedIndex;
            if (i == 0)
                return;
            string item_up = (string)Lbox_Class.Items[i - 1];
            Lbox_Class.Items[i] = item_up;
            Lbox_Class.Items[i - 1] = item;
            Lbox_Class.SelectedIndex = i - 1;
        }

        private void But_Down_Click(object sender, EventArgs e)
        {
            if (Lbox_Class.SelectedIndex == -1)
                return;
            string item = Lbox_Class.SelectedItem.ToString();
            int i = Lbox_Class.SelectedIndex;
            if (i == Lbox_Class.Items.Count - 1)
                return;
            string item_down = (string)Lbox_Class.Items[i + 1];
            Lbox_Class.Items[i] = item_down;
            Lbox_Class.Items[i + 1] = item;
            Lbox_Class.SelectedIndex = i + 1;
        }

        private void But_Del_Click(object sender, EventArgs e)
        {
            if (Lbox_Class.SelectedIndex == -1)
                return;
            int i = Lbox_Class.SelectedIndex;
            delstr.Add(Lbox_Class.SelectedItem.ToString());
            Lbox_Class.Items.Remove(Lbox_Class.SelectedItem.ToString());

            Lbox_Class.SelectedIndex = i - 1;
        }

        private void But_Done_Click(object sender, EventArgs e)
        {
            string str = "";
            foreach (var item in Lbox_Class.Items)
                str += item.ToString() + "|";
            str = str.Remove(str.Length - 1);
            if (str == "")
                str = "defualt";
            else
                retstr = str;
            Console.WriteLine(str);
            ChangXmlFile();

            this.Hide();
            this.Close();
        }

        private void ChangXmlFile()
        {
            ClsXMLoperate clsXM = Program.CreaterXMLHelper();

            foreach (var item in Lbox_Class.Items)
                if (!clsXM.CheckNode("UserInfo/" + item.ToString()))//否存在
                    clsXM.InsertSingleNode("UserInfo", item.ToString());
            if (delstr.Count == 0)
                return;
            var ret = MessageBox.Show("您 确定要删除账号分组吗？\r\n分组下记录的账号将全部删除！", "警告", MessageBoxButtons.YesNo);
            if (ret == DialogResult.Yes)
                foreach (var item in delstr)
                    if (clsXM.CheckNode("UserInfo/" + item.ToString()))//存在
                        clsXM.Delete("UserInfo", "UserInfo/" + item.ToString());
        }
    }
}