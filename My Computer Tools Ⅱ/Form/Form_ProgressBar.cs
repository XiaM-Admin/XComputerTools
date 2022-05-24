using System;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public partial class Form_ProgressBar : Form
    {
        public Form_ProgressBar(string txt, string tip, int maxcount, Form form)
        {
            InitializeComponent();
        }

        public void SetProgressForm(object obj)
        {
            ProgressData data = (ProgressData)obj;
            this.Invoke(new Action(() =>
            {
                this.Text = data.FormName;
                this.Lab_ProgressTip.Text = data.FormPGTip;
                this.Lab_Tip.Text = data.FormText;
                this.Progress1.Value = Convert.ToInt32(data.ProgressNowValue);
                this.Progress1.Maximum = Convert.ToInt32(data.ProgressMaxValue);
                if (Progress1.Value == Progress1.Maximum)
                    but_Close.Visible = true;
                this.Update();
                Progress1.Update();
            }));
        }

        public void AddProgress()
        {
            this.Invoke(new Action(() =>
            {
                Progress1.Value++;
            }));

            if (Progress1.Value == Progress1.Maximum)
                this.Hide();
        }

        public Fun_delegate Show(Form form)
        {
            but_Close.Visible = false;
            this.Show();
            return new Fun_delegate(SetProgressForm);
        }

        private void but_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }

    public class ProgressData
    {
        /// <summary>
        /// 窗口标题
        /// </summary>
        public string FormName;

        /// <summary>
        /// 窗口主提示
        /// </summary>
        public string FormText;

        /// <summary>
        /// 当前进度
        /// </summary>
        public long ProgressNowValue;

        /// <summary>
        /// 总进度
        /// </summary>
        public long ProgressMaxValue;

        /// <summary>
        /// 窗口副提示
        /// </summary>
        public string FormPGTip;
    }
}