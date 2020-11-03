using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Transactions_Microservice.Helper;
using Transactions_Microservice.Models;
using Transactions_Microservice.Provider;
using Transactions_Microservice.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Transactions_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private IProvider _provider;
        Client obj = new Client();
        static int cnt = 1234;
        public TransactionController(IProvider provider)
        {
            _provider = provider;
        }

        TransactionStatus status = new TransactionStatus()
        {
            message = "Completed",
            source_balance = 2500,
            destination_balance = 3500
        };

        // GET: api/<TransactionController>
        [HttpGet("getTransactions")]
        public List<TransactionHistory> getTransactions(int CustomerId)
        {
            return _provider.GetTransactionHistory(CustomerId);
        }

       /* // GET api/<TransactionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
       */

        // POST api/<TransactionController>
        [HttpPost("deposit")]
        public async Task<TransactionStatus> deposit([FromBody] dynamic model/*int AccountId, int amount*/)
        {
            HttpClient client = obj.AccountDetails();

            HttpResponseMessage response = client.GetAsync("api/Account/getAccount/?AccountId=" + model.AccountId).Result;

            var result = response.Content.ReadAsStringAsync().Result;
            Account acc = JsonConvert.DeserializeObject<Account>(result);
            
            
            HttpResponseMessage response1 = client.PostAsJsonAsync("api/Account/deposit",new {AccountID = Convert.ToInt32(model.AccountId), Amount = Convert.ToInt32(model.amount) }).Result;
            var result1 = response1.Content.ReadAsStringAsync().Result;
            TransactionStatus st = JsonConvert.DeserializeObject<TransactionStatus>(result1);

            cnt = cnt + 256;
            TransactionHistory history = new TransactionHistory()
            {
                TransactionId = cnt,
                AccountId = Convert.ToInt32(model.AccountId),
                message = st.message,
                source_balance = st.source_balance,
                destination_balance = st.destination_balance,
                DateOfTransaction = DateTime.Now,
                CustomerId = acc.CustomerId
            };

            _provider.AddToTransactionHistory(history);

            return st;
        }

        // POST api/<TransactionController>
        [HttpPost("withdraw")]
        public async Task<TransactionStatus> withdraw([FromBody] dynamic model/*int AccountId, int amount*/)
        {
            HttpClient client = obj.AccountDetails();

            HttpResponseMessage response = client.GetAsync("api/Account/getAccount/?AccountId=" + model.AccountId).Result;

            var result = response.Content.ReadAsStringAsync().Result;
            Account acc = JsonConvert.DeserializeObject<Account>(result);


            HttpResponseMessage response1 = client.PostAsJsonAsync("api/Account/withdraw", new { AccountID = Convert.ToInt32(model.AccountId), Amount = Convert.ToInt32(model.amount) }).Result;
            var result1 = response1.Content.ReadAsStringAsync().Result;
            TransactionStatus st = JsonConvert.DeserializeObject<TransactionStatus>(result1);

            cnt = cnt + 256;
            TransactionHistory history = new TransactionHistory()
            {
                TransactionId = cnt,
                AccountId = Convert.ToInt32(model.AccountId),
                message = st.message,
                source_balance = st.source_balance,
                destination_balance = st.destination_balance,
                DateOfTransaction = DateTime.Now,
                CustomerId = acc.CustomerId
            };

            _provider.AddToTransactionHistory(history);

            return st;
        }

        // POST api/<TransactionController>
        [HttpPost("transfer")]
        public async Task<TransactionStatus> transfer([FromBody] dynamic model/*int Source_AccountId, int Target_AccountId, int amount*/)
        {
            TransactionStatus st = new TransactionStatus();
            st.message = "Transfered from Account no. " + model.Source_AccountId + " To Account no. " + model.Target_AccountId;

            TransactionStatus st1 = withdraw(new { AccountId = Convert.ToInt32(model.Source_AccountId), amount = Convert.ToInt32(model.amount) }).Result;
            st.source_balance = st1.destination_balance;

            TransactionStatus st2 = deposit(new { AccountId = Convert.ToInt32(model.Target_AccountId), amount = Convert.ToInt32(model.amount) }).Result;
            st.destination_balance = st2.destination_balance ;

            return st;
        }
    }
}
