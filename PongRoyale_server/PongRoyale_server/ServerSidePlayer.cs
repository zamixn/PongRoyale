﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace PongRoyale_server
{
    public class ServerSidePlayer
    {
        public byte Id { get; private set; }
        public TcpClient TcpClient;

        public ServerSidePlayer(byte id, TcpClient client)
        {
            Id = id;
            TcpClient = client;
        }
    }
}
