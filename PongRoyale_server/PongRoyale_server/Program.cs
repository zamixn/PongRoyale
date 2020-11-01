using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PongRoyale_server
{
    class Program
    {

        static void Main(string[] args)
        {
            ServerController.Instance.Start();
        }

    }
}
