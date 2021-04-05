using NetworkChat.Model;
using NetworkChat.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace NetworkChat
{
    public partial class Chat : Form
    {
        public System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        public static string UserName { get; set; }
        public Client client = new Client();
        public static List<string> list = new List<string>();
        public static List<string> messageList = new List<string>();

        public Chat()
        {
            InitializeComponent();
        }
        public void Chat_Load(object sender, EventArgs e)
        {
            timer.Tick += Timer_Tick1;
            timer.Interval = 100;
            timer.Start();
        }
        private void Timer_Tick1(object sender, EventArgs e)
        {
            var mes = client.Start("3", null, null);
            var result = JsonConvert.DeserializeObject<List<string>>(mes);
            list = listBox1.Items.Cast<object>().Select(obj => obj.ToString()).ToList();
            if (!list.SequenceEqual(result))
            {
                listBox1.Items.Clear();
                foreach (var item in result)
                {
                    listBox1.Items.Add(item);
                }
            }
            var listMessage = client.Start("6", null, null);
            var res = JsonConvert.DeserializeObject<List<string>>(listMessage);
            messageList = listBox2.Items.Cast<object>().Select(obj => obj.ToString()).ToList();
            if (!messageList.SequenceEqual(res))
            {
                listBox2.Items.Clear();
                foreach (var item in res)
                {
                    
                    listBox2.Items.Add(item);
                    int visibleItems = listBox2.ClientSize.Height / listBox2.ItemHeight;
                    listBox2.TopIndex = Math.Max(listBox2.Items.Count - visibleItems + 1, 0);
                }
            }
        }


        public void sendMessage_Click(object sender, EventArgs e)
        {
            client.Start("5", null, UserName + ": " + tbMessage.Text.ToString());
            tbMessage.Clear();
        }
    }
}
