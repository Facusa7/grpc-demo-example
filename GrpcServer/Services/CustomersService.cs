using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Juan";
                output.LastName = "Romero";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Juana";
                output.LastName = "Doe";
            }
            else
            {
                output.FirstName = "Juane";
                output.LastName = "Romere";
            }

            //the "from result" allows to return a task type and it can either be sync or async code. 
            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(GetNewCustomersRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            var customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Facundo",
                    LastName = "Sa",
                    EmailAddress = "facundo.sa@test.com",
                    Age = 28,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Juan",
                    LastName = "Sa",
                    EmailAddress = "juan.sa@test.com",
                    Age = 30,
                    IsAlive = false
                },
                new CustomerModel
                {
                    FirstName = "Sophie",
                    LastName = "Rosemary",
                    EmailAddress = "sophieRosemary@test.com",
                    Age = 28,
                    IsAlive = true
                }
            };

            foreach (var customer in customers)
            {
                await Task.Delay(5000); //Just to make it clear how the stream goes.
                //this way one customer at a time is sent to the GRPC client
                await responseStream.WriteAsync(customer);
            }
            // When the execution reaches this point, the client will end up the process.
            // Otherwise, it will continue to listen until it is done.
        }
    }
}
