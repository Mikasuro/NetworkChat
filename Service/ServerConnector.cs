﻿using System;
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

        // У нас теперь есть объект-блокировка для синхронизации потоков во
        // время первого доступа к Одиночке.
        private static readonly object _lock = new object();

        public static ServerConnector GetInstance()
        {
            // Это условие нужно для того, чтобы не стопорить потоки блокировкой
            // после того как объект-одиночка уже создан.
            if (_instance == null)
            {
                // Теперь представьте, что программа была только-только
                // запущена. Объекта-одиночки ещё никто не создавал, поэтому
                // несколько потоков вполне могли одновременно пройти через
                // предыдущее условие и достигнуть блокировки. Самый быстрый
                // поток поставит блокировку и двинется внутрь секции, пока
                // другие будут здесь его ожидать.
                lock (_lock)
                {
                    // Первый поток достигает этого условия и проходит внутрь,
                    // создавая объект-одиночку. Как только этот поток покинет
                    // секцию и освободит блокировку, следующий поток может
                    // снова установить блокировку и зайти внутрь. Однако теперь
                    // экземпляр одиночки уже будет создан и поток не сможет
                    // пройти через это условие, а значит новый объект не будет
                    // создан.
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

        // Мы используем это поле, чтобы доказать, что наш Одиночка
        // действительно работает.
        public TcpClient Client { get; set; }
    }
}
