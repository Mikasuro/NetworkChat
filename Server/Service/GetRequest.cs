using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Service
{
    public class GetRequest
    {
        public static RequestHandler request = new RequestHandler();
        public static string Get(NetworkStream stream, string message)
        {
            stream.Write(Encoding.UTF8.GetBytes("2"), 0, Encoding.UTF8.GetBytes("2").Length);
            byte[] dataUser = new byte[256];
            int bytesUser = stream.Read(dataUser, 0, dataUser.Length);
            var resultUser = Encoding.UTF8.GetString(dataUser, 0, bytesUser);
            var result = request.Start(message, resultUser);
            string user;
            if (result != string.Empty)
            {
                stream.Write(Encoding.UTF8.GetBytes("true"), 0, Encoding.UTF8.GetBytes("true").Length);
                user = result;
            }
            else
            {
                stream.Write(Encoding.UTF8.GetBytes("false"), 0, Encoding.UTF8.GetBytes("false").Length);
                user = result;
            }
            return user;
        }
    }
}
