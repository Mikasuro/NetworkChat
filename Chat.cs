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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace NetworkChat
{
    public partial class Chat : Form
    {
        private System.Windows.Forms.Timer timer;
        public static string Message { get; set; }
        public Client client = new Client();
        public static List<string> list = new List<string>();
        public static Dispatcher dispatcher;
        public static List<string> messageList = new List<string>();


        public Chat()
        {
            InitializeComponent();
        }

        private void Chat_Load(object sender, EventArgs e)
        {
            var message = client.Start("3", null, null);
            var result = JsonConvert.DeserializeObject<List<string>>(message);
            foreach (var item in result)
            {
                listBox1.Items.Add(item);
            }
            Thread thread = new Thread(new ThreadStart(() =>
            {
                client.Start("6", null, null);
            }));
            thread.Start();

        }


        private void sendMessage_Click(object sender, EventArgs e)
        {
            MessageForChat message = new MessageForChat();
            message.Id = "5";
            message.Message = tbMessage.Text;
            var result = JsonConvert.SerializeObject(message);
            client.Start(message.Id, null, result);
            listView1.Items.Add(Message);
            tbMessage.Clear();
        }
    }
}
