using Grpc.Net.Client;
using MailgRPC.Server;

namespace MailgRPC.Client
{
    public static class Program
    {
        private const string GRPC_SERVER = "http://localhost:5243";

        public static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Press any key to start");
                Console.ReadKey();

                using var channel = GrpcChannel.ForAddress(GRPC_SERVER);
                var client = new Mailing.MailingClient(channel);

                var request = new MailRequest
                {
                    Receiver = "lucas@eu.com",
                    Subject = "Hello from gRPC Service!",
                    Body = "Hi, these emais has sended by gRPC service with EscNet package!"
                };

                var reply = await client.SendMailAsync(request);

                Console.WriteLine(reply.Message);
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }
    }
}