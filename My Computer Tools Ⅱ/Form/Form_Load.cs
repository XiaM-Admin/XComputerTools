using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using settings = My_Computer_Tools_Ⅱ.Properties.Settings;
using My_Computer_Tools_Ⅱ.NetAPI;

namespace My_Computer_Tools_Ⅱ
{
    public partial class Form_Load : Form
    {
        public Form_Load()
        {
            InitializeComponent();
        }
        public bool isReLogin = false;
        public bool ReLogin_Ret = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            Text_TipsGG.Text = NetApiCommand.Net_GetGG();//获取公告
            Text_User.Text = settings.Default.LoadName;
            Text_Pwd.Text = settings.Default.LoadPwd;
        }
                                         
        private void Lab_GotoMain_Click(object sender, EventArgs e)
        {
            if (isReLogin)
                return;

            DialogResult ret = MessageBox.Show("确定跳过吗？\r\n跳过登录，部分功能不可用。\r\n您可在主界面状态栏中重新进行登陆操作。","提示",MessageBoxButtons.OKCancel);
            if (ret == DialogResult.OK)
            {
                settings.Default.ShowLoad = false;
                settings.Default.Save();
                this.Hide();
                Form_Main frm = new Form_Main();
                frm.ShowDialog();
                this.Close();
            }
        }

        private void Lab_Register_Click(object sender, EventArgs e)
        {
            using (Register form = new Register() )
            {
                this.Hide();
                form.ShowDialog();
                Text_User.Text = form.UserName;
                Text_Pwd.Text = form.Password;
                this.Show();
            }
        }

        private void But_Load_Click(object sender, EventArgs e)
        {
            if (Text_Pwd.TextLength>=0 && Text_User.TextLength>=0)
            {
                var ret = NetApiCommand.Net_Login(Text_User.Text, Text_Pwd.Text);
                if (ret)
                {
                    settings.Default.ShowLoad = false;
                    settings.Default.Save();
                    if (isReLogin)
                    {
                        this.Hide();
                        ReLogin_Ret = true;
                        this.Close();
                    }
                    else
                    {
                        this.Hide();
                        Form_Main frm = new Form_Main();
                        frm.ShowDialog();
                        this.Close();
                    }
                }
            }

        }
    }
}
