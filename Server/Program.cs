using Newtonsoft.Json;
using Server.Model;
using Server.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Server
{
    class Program
    {
        private const int port = 8888;
        public static Dictionary<string, TcpClient> dictionary = new Dictionary<string, TcpClient>();
        public static List<string> list = new List<string>();
        public static List<string> listMessage = new List<string>();
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                server.Start();
                string result = string.Empty;
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Thread thread = new Thread(new ThreadStart(() =>
                    {
                        Work(client);
                    }));
                    thread.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
            }
        }
        public static string Work(TcpClient client)
        {
            string user = string.Empty;
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[256];
            string message;
            while (true)
            {
                try
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    message = Encoding.UTF8.GetString(data, 0, bytes);
                    if (message == "1")
                    {
                        user = GetRequest.Get(stream, message);
                    }
                    if (message == "2")
                    {
                        user = GetRequest.Get(stream, message);
                    }
                    if (message == "3")
                    {
                        if (!dictionary.ContainsKey(user) && (user != string.Empty))
                        {
                            dictionary.Add(user, client);
                            list.Add(user);
                        }
                        string datalist = JsonConvert.SerializeObject(list);
                        stream.Write(Encoding.UTF8.GetBytes(datalist), 0, Encoding.UTF8.GetBytes(datalist).Length);
                    }
                    if (message == "5")
                    {
                        stream.Write(Encoding.UTF8.GetBytes("2"), 0, Encoding.UTF8.GetBytes("2").Length);
                        byte[] dataMessage = new byte[Int16.MaxValue];
                        var byteMessage = stream.Read(dataMessage, 0, dataMessage.Length);
                        listMessage.Add(Encoding.Default.GetString(dataMessage, 0, byteMessage));
                    }
                    if (message == "6")
                    {
                        string datalistMessage = JsonConvert.SerializeObject(listMessage);
                        stream.Write(Encoding.UTF8.GetBytes(datalistMessage), 0, Encoding.UTF8.GetBytes(datalistMessage).Length);
                    }
                }
                catch
                {
                    if (dictionary.ContainsValue(client))
                    {
                        var key = dictionary.Where(x => x.Value == client).FirstOrDefault().Key;
                        dictionary.Remove(key);
                        list.Remove(key);
                    }
                }
            }
        }
    }
}
