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
        
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                server.Start();
                string result = string.Empty;
                WorkWithClient withClient = new WorkWithClient();
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Thread thread = new Thread(new ThreadStart(() =>
                    {
                        withClient.Work(client);
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
            }
        }
    }
}
