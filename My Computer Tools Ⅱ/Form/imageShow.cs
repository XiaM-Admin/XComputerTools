using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public partial class imageShow : Form
    {
        private ListView listView;
        private Dictionary<string, string> Imgmap = new Dictionary<string, string>();
        private string Cdnhost = "";

        /// <summary>
        /// 如果第一个参数为null
        /// 那么第二个直接填写链接路径
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="cdnhost"></param>
        public imageShow(ListView listView, string cdnhost)
        {
            InitializeComponent();
            this.listView = listView;
            this.Cdnhost = cdnhost;
        }

        private void imageShow_Load(object sender, EventArgs e)
        {
            if (listView == null)
            {
                listBox.Items.Add("picname");
                Imgmap.Add("picname", Cdnhost);
                listBox.SelectedIndex = 0;
                lab_tip.Text = "TIP：初始化完成...";
                return;
            }
            int showi = 0;
            foreach (ListViewItem item in listView.Items)
            {
                Console.WriteLine(item.Text);
                if (item.Text.Contains("png") || item.Text.Contains("jpg") || item.Text.Contains("bmp"))
                {
                    string url = "http://" + Cdnhost + "/" + item.SubItems[1].Text + item.SubItems[0].Text;
                    Imgmap.Add(item.Text, url);
                    showi++;
                }
            }
            List<long> longs = new List<long>();
            Dictionary<long, string> valuePairs = new Dictionary<long, string>();
            //按时间排序 最新的放listbox前面
            foreach (var item in Imgmap)
            {
                //取前缀
                try
                {
                    longs.Add(Convert.ToInt64(item.Key.Split('.')[0]));
                    valuePairs.Add(Convert.ToInt64(item.Key.Split('.')[0]), item.Key);
                }
                catch (Exception)
                {
                    listBox.Items.Add(item.Key);
                }
            }

            longs.Sort();
            //写入listbox
            foreach (var item in longs)
            {
                listBox.Items.Insert(0, valuePairs[item]);
            }
            lab_tip.Text = "TIP：初始化完成...";
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            picbox.ImageLocation = Imgmap[listBox.SelectedItem.ToString()];
            BBCodetxt.Text = $"[URL={Imgmap[listBox.SelectedItem.ToString()]}][IMG]{Imgmap[listBox.SelectedItem.ToString()]}[/IMG][/URL]";
            HTMLtxt.Text = $"<a href=\"{Imgmap[listBox.SelectedItem.ToString()]}\" target=\"_blank\"><img src=\"{Imgmap[listBox.SelectedItem.ToString()]}\" alt=\"{listBox.SelectedItem.ToString()}\"></a>";
            MDtxt.Text = $"![{listBox.SelectedItem}]({Imgmap[listBox.SelectedItem.ToString()]})";
            imgurltxt.Text = picbox.ImageLocation;
        }

        private void BBCodetxt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Clipboard.SetText(textBox.Text);
            lab_tip.Text = $"TIP：您刚刚复制了 {textBox.Text} 到剪贴板中...";
        }
    }
}