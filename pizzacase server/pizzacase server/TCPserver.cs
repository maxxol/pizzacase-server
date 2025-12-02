using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace pizzacase_server
{
    internal class TCPserver
    {
        public static async Task StartTCPAsync()
        {
            string privateKey = "p4X9uD1fL7qW2sN8rC5tJ0vH6yZ3kM1b"; // placeholder
            var listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            Console.WriteLine("Server started. Waiting for clients...");

            while (true) // keep server running
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine("Client connected!");
                _ = HandleClientAsync(client, privateKey);
                _ = HandleServerInputAsync(client); // new message sender
            }
        }

        private static async Task HandleClientAsync(TcpClient client, string privateKey)
        {
            using var stream = client.GetStream();

            while (true)
            {
                // Read 4-byte length prefix
                byte[] lengthBuf = new byte[4];
                int read = await stream.ReadAsync(lengthBuf, 0, 4);
                if (read == 0) return;

                int messageLength = BitConverter.ToInt32(lengthBuf, 0);

                // Read messageLength bytes
                byte[] payload = new byte[messageLength];
                int totalRead = 0;

                while (totalRead < messageLength)
                {
                    int n = await stream.ReadAsync(payload, totalRead, messageLength - totalRead);
                    if (n == 0)
                        throw new Exception("Client disconnected mid-message.");

                    totalRead += n;
                }

                // Convert UTF-8 → Base64 string
                string base64Cipher = Encoding.UTF8.GetString(payload);

                // Base64 → AES bytes
                string decrypted = DataDecryptor.DecryptData(base64Cipher, privateKey);

                Console.WriteLine("Decrypted: \n" + decrypted);
            }
        }



        // New method: lets the server send messages anytime
        private static async Task HandleServerInputAsync(TcpClient client)
        {
            using var stream = client.GetStream();
            while (true)
            {
                string message = Console.ReadLine(); // server operator types message
                if (string.IsNullOrEmpty(message)) continue;

                byte[] data = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);
                Console.WriteLine("Sent: " + message);
            }
        }
    }
}
