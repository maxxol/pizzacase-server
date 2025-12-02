using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzacase_server
{
    internal class ProtocolSelector
    {
        public static void StartServer(string protocol)
        {

            if (protocol.ToLower() == "udp")
            {
                UDPserver.startUDP();
            }
            if (protocol.ToLower() == "tcp")
            {
                TCPserver.StartTCPAsync();
            }
            else
            {
                Console.WriteLine("no supported protocol provided");
            }
        }
    }
}
