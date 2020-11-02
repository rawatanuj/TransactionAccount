using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Transactions_Microservice.Helper
{
    public class Client
    {
        public HttpClient AccountDetails()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:53086");
            return client;
        }

    }
}
