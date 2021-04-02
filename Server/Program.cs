using Server.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        private const int port = 8888;
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                Dictionary<string, TcpClient> user = new Dictionary<string, TcpClient>();
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                server.Start();
                string result = string.Empty;
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Thread thread = new Thread(new ThreadStart(() => 
                    { 
                        result = WorkWithClient(client);
                        if (result != string.Empty)
                        {
                            user.Add(result, client);
                        }
                    }));
                    thread.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }
       
        public static string WorkWithClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length);
            string message = Encoding.UTF8.GetString(data, 0, bytes);
            string user = string.Empty;
            RequestHandler request = new RequestHandler();
            if (message == "1")
            {
                stream.Write(Encoding.UTF8.GetBytes("2"), 0, Encoding.UTF8.GetBytes("2").Length);
                byte[] dataUser = new byte[256];
                int bytesUser = stream.Read(dataUser, 0, dataUser.Length);
                var resultUser = Encoding.UTF8.GetString(dataUser, 0, bytesUser);
                var result = request.Start(resultUser);
                if (result != null)
                {
                    stream.Write(Encoding.UTF8.GetBytes("5"), 0, Encoding.UTF8.GetBytes("5").Length);
                    user = result;
                }
            }
            if (message == "2")
            {

            }
            return user;
        }
    }
}
