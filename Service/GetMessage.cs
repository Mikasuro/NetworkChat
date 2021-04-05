using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkChat.Service
{
    public class GetMessage
    {
        public static string Get(NetworkStream stream, string message)
        {
            stream.Write(Encoding.UTF8.GetBytes(message), 0, Encoding.UTF8.GetBytes(message).Length);
            byte[] datalistMessage = new byte[5000000];
            stream.Read(datalistMessage, 0, datalistMessage.Length);
            return Encoding.UTF8.GetString(datalistMessage);
        }
    }
}
