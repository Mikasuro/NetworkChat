using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkChat.Service
{
    public class SendRequest
    {
        public static bool result;
        public static string Send(NetworkStream stream, string message, string user, string id)
        {
            byte[] dataMessage = Encoding.UTF8.GetBytes(id);
            stream.Write(dataMessage, 0, dataMessage.Length);
            byte[] dataRead = new byte[256];
            int bytes = stream.Read(dataRead, 0, dataRead.Length);
            message = Encoding.UTF8.GetString(dataRead, 0, bytes);
            if (message == "2")
            {
                byte[] dataUser = Encoding.UTF8.GetBytes(user);
                stream.Write(dataUser, 0, dataUser.Length);
            }
            byte[] dataResult = new byte[256];
            int bytesResult = stream.Read(dataResult, 0, dataResult.Length);
            message = Encoding.UTF8.GetString(dataResult, 0, bytesResult);
            if (message == "true")
            {
                result = true;
            }
            else result = false;
            while (stream.DataAvailable);
            return message;
        }
        
            
    }
}
