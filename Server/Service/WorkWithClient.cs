using Newtonsoft.Json;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Server.Service
{
    public class WorkWithClient
    {
        public Dictionary<string, TcpClient> dictionary = new Dictionary<string, TcpClient>();
        public List<string> list = new List<string>();
        public string Work(TcpClient client)
        {
            string user = string.Empty;
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[256];
            string message;
            int bytes;
            while (true)
            {
                bytes = stream.Read(data, 0, data.Length);
                message = Encoding.UTF8.GetString(data, 0, bytes);
                if (message == "1")
                {
                    user = GetRequest.Get(stream, message);
                }
                if (message == "2")
                {
                    user = GetRequest.Get(stream, message);
                }
                if (!dictionary.ContainsKey(user) && (user != string.Empty))
                {
                    dictionary.Add(user, client);
                    list.Add(user);
                }
                if (message == "3")
                {
                    string datalist = JsonConvert.SerializeObject(list);
                    stream.Write(Encoding.UTF8.GetBytes(datalist), 0, Encoding.UTF8.GetBytes(datalist).Length);
                }
                try
                {
                    if (JsonConvert.DeserializeObject<MessageForChat>(message).Id == "5")
                    {
                        var result = JsonConvert.DeserializeObject<MessageForChat>(message);
                        var bytesResult = Encoding.UTF8.GetBytes(result.Message);
                        foreach (var item in dictionary)
                        {
                            var clients = item.Value.GetStream();
                            clients.Write(bytesResult, 0, bytesResult.Length);
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
