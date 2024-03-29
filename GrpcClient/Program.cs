﻿using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest { Name = "Facundo" };
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");

            ////Here the greeter server is not being instantiated. The object is being created through the channel.
            //var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(input);

            //Console.WriteLine(reply.Message); //This is what the server responds 

            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            //Here the greeter server is not being instantiated. The object is being created through the channel.
            var customerClient = new Customer.CustomerClient(channel);

            var input = new CustomerLookupModel { UserId = 2 };

            var customer = await customerClient.GetCustomerInfoAsync(input);

            Console.WriteLine($"{customer.FirstName} {customer.LastName}"); //This is what the server responds 

            Console.WriteLine();
            Console.WriteLine("New Customer List");
            Console.WriteLine();
            using (var call = customerClient.GetNewCustomers(new GetNewCustomersRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName}: {currentCustomer.EmailAddress}");
                }
            }

            Console.ReadLine();
        }
    }
}
