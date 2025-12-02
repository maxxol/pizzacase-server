namespace pizzacase_server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await TCPserver.StartTCPAsync();
            // Or keep running manually if StartTCPAsync doesn't block
            Console.WriteLine("Server started. Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
