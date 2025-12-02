using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace pizzacase_server
{
    internal class UDPserver
    {
        public static void startUDP() 
        {

            var udp = new UdpClient(5000);

            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = udp.Receive(ref ep);

            string received = Encoding.UTF8.GetString(data);
            Console.WriteLine(received);

        }
    }
}
