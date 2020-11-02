using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Transactions_Microservice.Helper;
using Transactions_Microservice.Models;
using Transactions_Microservice.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Transactions_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private IRepository _repo;
        Client obj = new Client();
        public TransactionController(IRepository repo)
        {
            _repo = repo;
        }

        TransactionStatus status = new TransactionStatus()
        {
            message = "Completed",
            source_balance = 2500,
            destination_balance = 3500
        };

        // GET: api/<TransactionController>
        [HttpGet("getTransactions")]
        public TransactionHistory getTransactions(int Customer_ID)
        {
            
            return new TransactionHistory()
            {
                AccountId = 1234,
                CounterpartiesId = 1245,
                message = "Completed",
                source_balance = 2050,
                destination_balance = 3050,
                DateOfTransaction = DateTime.Now
            };
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
        public async Task<TransactionStatus> /*List<TransactionHistory>*/ deposit([FromQuery] int AccountId, float amount)
        {
            HttpClient client = obj.AccountDetails();

            HttpResponseMessage response = await client.GetAsync("api/Account/getAccount/{AccountId}");

           
            var result = response.Content.ReadAsStringAsync().Result;
            Account acc = JsonConvert.DeserializeObject<Account>(result);

            HttpResponseMessage response1 = client.PostAsJsonAsync("api/Account/deposit/?AccountId=" + AccountId, amount).Result;
            var result1 = response1.Content.ReadAsStringAsync().Result;
            TransactionStatus st = JsonConvert.DeserializeObject<TransactionStatus>(result);
           
            TransactionHistory history = new TransactionHistory()
            { 
              TransactionId =1234,
              AccountId = AccountId,
              message = "Completed",
              CounterpartiesId = 0,
              source_balance = 2500,
              destination_balance = 3500,
              DateOfTransaction = DateTime.Now,
              CustomerId = 10
            };

            _repo.AddToTransactionHistory(history);


            return new TransactionStatus() {
                message="Completed",
                source_balance=2500,
                destination_balance=3500
            };
        }

        // POST api/<TransactionController>
        [HttpPost("withdraw")]
        public TransactionStatus withdraw(int AccountId, float amount)
        {
            return new TransactionStatus(){
              message = "Completed", 
              source_balance = 2500, 
              destination_balance = 3500 
            };
        }

        // POST api/<TransactionController>
        [HttpPost("transfer")]
        public TransactionStatus transfer(int Source_AccountId, int Target_AccountId, float amount)
        {
            return new TransactionStatus() { 
                message = "Completed", 
                source_balance = 2500, 
                destination_balance = 3500 
            };
        }

        /* // PUT api/<TransactionController>/5
         [HttpPut("{id}")]
         public void Put(int id, [FromBody] string value)
         {
         }

         // DELETE api/<TransactionController>/5
         [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }
        */
    }
}
