using System;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public partial class Control_Show : UserControl
    {
        private string _userpwd;
        private readonly string _class;
        private Fun_delegate CMBSinit;

        public Control_Show(string classstr, string lab1, string lab2, string lab3, Fun_delegate cmbsinit)
        {
            InitializeComponent();
            _class = classstr;
            lab_Name.Text = lab1;
            Lab_User.Text = lab2;
            this.CMBSinit = cmbsinit;
            Lab_Userpwd.Text = "************";
            _userpwd = lab3;
        }

        private void Control_Show_Load(object sender, EventArgs e)
        {
        }

        private void but_ShowHide_Click(object sender, EventArgs e)
        {
            switch (but_ShowHide.Text)
            {
                case "S":
                    //显示
                    Lab_Userpwd.Text = _userpwd;
                    but_ShowHide.Text = "H";
                    break;

                case "H":
                    //隐藏
                    Lab_Userpwd.Text = "************";
                    but_ShowHide.Text = "S";
                    break;

                default:
                    break;
            }
        }

        private void Lab_User_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //导入剪贴板
            Clipboard.SetText(Lab_User.Text);
            Program.WinCommand.ChangeTips("账号本本", "账号已复制到剪贴板", 3);
        }

        private void Lab_Userpwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //导入密码到剪贴板
            Clipboard.SetText(_userpwd);
            Program.WinCommand.ChangeTips("密码本本", "密码已复制到剪贴板", 3);
        }

        /// <summary>
        /// 快捷修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_ChangPwd_Click(object sender, EventArgs e)
        {
            using (Form_input input = new Form_input("请输入您要更改的密码："))
            {
                input.ShowDialog();
                string str = input.tbox_input.Text;
                if (str == "")
                    return;
                string Accstr = Lab_User.Text + "^" + str;

                //修改xml文件
                ClsXMLoperate clsXM = Program.CreaterXMLHelper();

                var ret = clsXM.UpdateXmlNode("UserInfo/" + _class + "/" + lab_Name.Text, Accstr);

                if (ret)
                {
                    MessageBox.Show("修改密码完毕！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _userpwd = str;
                    CMBSinit(null);
                }
                else
                    MessageBox.Show("修改密码未完成，请重试...", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}