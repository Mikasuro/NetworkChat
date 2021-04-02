using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkChat.Service
{
    class Client
    {
        private const int port = 8888;
        private const string server = "127.0.0.1";
        private bool result;

        public bool Start(string message, string user)
        {
            TcpClient client = new TcpClient();
            client.Connect(server, port);
            NetworkStream stream = client.GetStream();
            if (message == "1")
            {
                byte[] dataMessage = Encoding.UTF8.GetBytes(message);
                stream.Write(dataMessage, 0, dataMessage.Length);
                byte[] dataRead = new byte[256];
                int bytes = stream.Read(dataRead, 0, dataRead.Length);
                message = Encoding.UTF8.GetString(dataRead, 0, bytes);
                if (message == "2")
                {
                    byte[] dataUser = Encoding.UTF8.GetBytes(user);
                    stream.Write(dataUser, 0, dataUser.Length);
                }
            }
            byte[] dataResult = new byte[256];
            int bytesResult = stream.Read(dataResult, 0, dataResult.Length);
            message = Encoding.UTF8.GetString(dataResult, 0, bytesResult);
            if (message == "5")
            {
                result = true;
            }
            else result = false;
            while (stream.DataAvailable) ;
            stream.Close();
            client.Close();
            return result;
        }
    }
}

