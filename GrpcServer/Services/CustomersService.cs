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
    }
}
