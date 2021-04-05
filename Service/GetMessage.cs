using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkChat.Service
{
    public class GetMessage
    {
        public static void Get(NetworkStream stream)
        {
            byte[] data = new byte[256];
            int bytes;
            while (true)
            {
                bytes = stream.Read(data, 0, data.Length);
                Chat.Message = Encoding.UTF8.GetString(data, 0, bytes);
            }
        }
    }
}
