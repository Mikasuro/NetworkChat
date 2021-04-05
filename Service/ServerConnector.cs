using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkChat
{
    public class ServerConnector
    {

        private const int port = 8888;
        private const string server = "127.0.0.1";
        private ServerConnector() { }
        private static ServerConnector _instance;
        private static readonly object _lock = new object();

        public static ServerConnector GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ServerConnector();
                        _instance.Client = new TcpClient();
                        _instance.Client.Connect(server, port);
                    }
                }
            }
            return _instance;
        }
        public TcpClient Client { get; set; }
    }
}
