using NetworkChat.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkChat.Service
{
    class GetList
    {
        public static string Get(NetworkStream stream, string message)
        {
            stream.Write(Encoding.UTF8.GetBytes(message), 0, Encoding.UTF8.GetBytes(message).Length);
            byte[] datalist = new byte[256];
            stream.Read(datalist, 0, datalist.Length);
            return Encoding.UTF8.GetString(datalist);
        }
    }
}
