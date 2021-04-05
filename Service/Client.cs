using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkChat.Service
{
    public class Client
    {
        public string Start(string id, string user, string message)
        {
            ServerConnector connector = ServerConnector.GetInstance();
            NetworkStream stream = connector.Client.GetStream();
            string request = string.Empty;
            try
            {
                if (id == "3")
                {
                    request = GetList.Get(stream, id);
                }
                if (id == "4")
                {
                    request = SendRequest.Send(stream, id, user, message);
                }
                if (id == "5")
                {
                    SendMessage.Send(stream, id, message);
                }
                if (id == "6")
                {
                    request = GetMessage.Get(stream, id);
                }
            }
            catch
            {

            }
            return request;
        }
    }
}

