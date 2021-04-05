using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkChat.Service
{
    public class SendMessage
    {
        public static void Send(NetworkStream stream,string id, string message)
        {
            stream.Write(Encoding.UTF8.GetBytes(id), 0, Encoding.UTF8.GetBytes(id).Length);
            byte[] data = new byte[Int16.MaxValue];
            var bytes = stream.Read(data, 0, data.Length);
            if(Encoding.UTF8.GetString(data, 0, bytes) == "2")
            {
                stream.Write(Encoding.Default.GetBytes(message), 0, message.Length);
            }
        }
    }
}
