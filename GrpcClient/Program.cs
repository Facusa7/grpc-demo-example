using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServer;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = new HelloRequest { Name = "Facundo" };
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            //Here the greeter server is not being instantiated. The object is being created through the channel.
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(input);

            Console.WriteLine(reply.Message); //This is what the server responds 

            Console.ReadLine();
        }
    }
}
