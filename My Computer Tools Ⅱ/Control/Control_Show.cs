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
    public partial class Control_Show : UserControl
    {
        public Control_Show(string lab1, string lab2, string lab3)
        {
            InitializeComponent();
            lab_Name.Text = lab1;
            Lab_User.Text = lab2;
            Lab_Userpwd.Text = lab3;
        }

        private void Control_Show_Load(object sender, EventArgs e)
        {

        }
    }
}
