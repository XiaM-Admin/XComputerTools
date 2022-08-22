using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public partial class Form_AccountControl : Form
    {
        private readonly string _classname;
        private readonly string _lab_Tip;

        //这里使用字典 进行本地索引数据处理
        //退出时操作保存文件
        private readonly Dictionary<string, string> AccountStrs = new Dictionary<string, string>();

        private readonly List<string> BackVs = new List<string>();

        private readonly Thread TipThread = null;

        public Form_AccountControl(string classname)
        {
            InitializeComponent();
            _classname = classname;
            lab_TipClass.Text += classname;
            _lab_Tip = lab_Tip.Text;

            //初始化账号列表
            lbox_AccList.Items.Clear();

            ClsXMLoperate clsXM = Program.CreaterXMLHelper();
            var Vsstr = clsXM.GetNodeVsStr("UserInfo/" + _classname);
            foreach (string str in Vsstr)
            {
                lbox_AccList.Items.Add(str);//添加到显示列表
                string userAcc = clsXM.GetNodeContent("UserInfo/" + _classname + "/" + str);
                AccountStrs.Add(str, userAcc);
                BackVs.Add(str);
            }
            TipThread = new Thread(Thread_Tip)
            {
                IsBackground = true
            };
            TipThread.Start();
        }

        private void Form_AccountControl_Load(object sender, EventArgs e)
        {
            Console.WriteLine(_classname);
        }

        private void But_Up_Click(object sender, EventArgs e)
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

        private void But_Down_Click(object sender, EventArgs e)
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

        private void But_Del_Click(object sender, EventArgs e)
        {
            if (lbox_AccList.SelectedIndex == -1)
                return;

            if (MessageBox.Show("注意删除操作，没有恢复途径！\r\n确定删除么?", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            int i = lbox_AccList.SelectedIndex;
            AccountStrs.Remove(lbox_AccList.SelectedItem.ToString());//删除账号
            lbox_AccList.Items.Remove(lbox_AccList.SelectedItem.ToString());
            lbox_AccList.SelectedIndex = i - 1;
            lab_Tip.Text = "Tip：账号数据已经删除！";
        }

        /// <summary>
        /// listbox切换选定账号时 刷新一下显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lbox_AccList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbox_AccList.SelectedIndex == -1)
                return;

            Text_Username.Text = lbox_AccList.SelectedItem.ToString();

            string[] vs = AccountStrs[lbox_AccList.SelectedItem.ToString()].Split(' ');
            if (vs.Length != 0)
            {
                Text_User.Text = vs[0];
                Text_UserPwd.Text = vs[1];
            }
        }

        /// <summary>
        /// 修改和添加账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_AddChang_Click(object sender, EventArgs e)
        {
            //检查编辑框合法
            if (Text_User.Text == "" || Text_Username.Text == "" || Text_UserPwd.Text == "")
            {
                MessageBox.Show("填入的账号信息部分为空，或不合法。\r\n请检查并且重新填写！", "错误");
                return;
            }

            //如果存在空格弹出提示
            if (Text_User.Text.Contains(" ") || Text_Username.Text.Contains(" ") || Text_UserPwd.Text.Contains(" "))
            {
                MessageBox.Show("填入的账号信息不能存在空格！\r\n请检查并且重新填写！", "错误");
                return;
            }

            //检查Text_Username.Text是否符合xml命名规则
            if (!Check_XmlName(Text_Username.Text))
            {
                MessageBox.Show("账号名称第一个字符不能为数字，请检查并且重新填写！", "错误");
                return;
            }

            string Accuserpwd = Text_User.Text + " " + Text_UserPwd.Text;
            //检查账号名字是否存在
            var ret = AccountStrs.ContainsKey(Text_Username.Text);

            if (ret)//存在 直接修改
            {
                AccountStrs[Text_Username.Text] = Accuserpwd;
                lab_Tip.Text = "Tip：账号数据已经修改完毕！";
            }
            else//不存在 创建
            {
                AccountStrs.Add(Text_Username.Text, Accuserpwd);
                lbox_AccList.Items.Add(Text_Username.Text);
                lab_Tip.Text = "Tip：新账号已经添加！";
            }
            //清空编辑框
            Text_User.Text = "";
            Text_UserPwd.Text = "";
            Text_Username.Text = "";
        }

        /// <summary>
        /// 检查xml元素的规则
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private bool Check_XmlName(string text)
        {
            try
            {
                Convert.ToInt16(text.PadLeft(1));
                return false;
            }
            catch (Exception)
            {
                return true;
                throw;
            }
        }

        //启动tip提示线程
        private void Thread_Tip()
        {
            int i = 4;
            while (true)
            {
                if (lab_Tip.Text != _lab_Tip)
                {
                    //执行修改字符操作
                    i--;
                    if (i == 0)
                    {
                        if (this.IsHandleCreated)
                            //委托操作
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                lab_Tip.Text = _lab_Tip;
                            }));
                        i = 4;
                    }
                }
                Thread.Sleep(999);
            }
        }

        /// <summary>
        /// 保存询问
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_AccountControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<string> Vs = new List<string>();
            //检查一下字典是否更改  没改就直接退出了...
            foreach (var item in AccountStrs)
                Vs.Add(item.Key);
            int i = 0;
            //判断两个List是否相等
            foreach (var item in Vs)
            {
                if (i == BackVs.Count || BackVs.Count != Vs.Count || item != BackVs[i])
                {
                    SaveAccountList();
                    return;
                }
                i++;
            }
        }

        /// <summary>
        /// 保存账号
        /// </summary>
        private void SaveAccountList()
        {
            //保存
            ClsXMLoperate clsXM = Program.CreaterXMLHelper();
            //先删除分类下的所有账号
            var Vsstr = clsXM.GetNodeVsStr("UserInfo/" + _classname);
            foreach (var item1 in Vsstr)
                clsXM.Delete("UserInfo/" + _classname, "UserInfo/" + _classname + "/" + item1);

            //再依次添加字典中的值
            foreach (var item1 in AccountStrs)
                clsXM.InsertSingleNode("UserInfo/" + _classname, item1.Key, item1.Value);
        }
    }
}