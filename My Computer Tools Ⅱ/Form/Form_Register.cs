using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using My_Computer_Tools_Ⅱ.NetAPI;


namespace My_Computer_Tools_Ⅱ
{
    public partial class Register : Form
    {
        public string UserName;
        public string Password;
        public Register()
        {
            InitializeComponent();
        }

        private void Form_Register_Load(object sender, EventArgs e)
        {

        }

        private void but_Register_Click(object sender, EventArgs e)
        {
            if (Text_UserName.Text != "" && Text_UserPwd.Text != "" && Text_Email.Text != "")
            {
                bool ret = NetApiCommand.Net_Reg(Text_UserName.Text, Text_UserPwd.Text, Text_Email.Text);
                if (ret == true)
                {
                    UserName = Text_UserName.Text;
                    Password = Text_UserPwd.Text;
                    this.Hide();
                }
            }
        }
    }
}
