using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account_Microservice.Models;
using Account_Microservice.Provider;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountProvider _Provider;

        public AccountController(IAccountProvider Provider)
        {
            _Provider = Provider;
        }

        [HttpPost("createAccount")]
        public AccountCreationStatus createAccount([FromBody]dynamic ob)
        {

            AccountCreationStatus acs = new AccountCreationStatus();
            acs = _Provider.AddAccount(Convert.ToInt32(ob.CustomerId), Convert.ToString(ob.AccountType));
            return acs;
        }

        [HttpGet("getCustomerAccounts")]
        public IEnumerable<Account> getCustomerAccounts(int CustomerId )
        {
            List<Account> Listaccount = new List<Account>();
            Listaccount = _Provider.getAllAccounts(CustomerId).ToList();
            return Listaccount;

            /*return new List<Account>{ new Account() {
                AccountId=1,
            CustomerId = 1,
            AccountType = "saving",
            Balance=1200
            } };*/

        } 

        [HttpGet("getAccount")]
        public Account getAccount(int AccountId)
        {

            Account a = new Account();
            a = _Provider.getCustomerAccount(AccountId);
            return a;

            /*return new Account(){
                AccountId=1,
            CustomerId = 3,
            AccountType = "saving",
            Balance=1200
            };*/
        }


        [HttpGet("getAccountStatement")]
        public IEnumerable<Statement> getAccountStatement([FromBody] dynamic obj)
        {
            List<Statement> statements = new List<Statement>();
            if ((obj.from_date == null)&&(obj.to_date == null))
            {
                DateTime? t = null;
                DateTime ? f = null;
                statements = _Provider.getStatement(Convert.ToInt32(obj.AccountId),Convert.ToDateTime(t),Convert.ToDateTime(f));
            
            }
            else
            {
                statements = _Provider.getStatement(Convert.ToInt32(obj.AccountId), Convert.ToDateTime(obj.from_date), Convert.ToDateTime(obj.to_date));
            }
            
           
            return statements;
            /*
            return new List<Statement>{ new Statement() {
                     AccountId = 1,
                    date=DateTime.UtcNow,
                    refno ="4587",
                    ValueDate =DateTime.UtcNow.AddDays(count),
                    Withdrawal=400,
                    Deposit=500,
                    ClosingBalance=1100
            } };*/

        }

        [HttpPost("deposit")]
        public TransactionStatus deposit([FromBody] dynamic obj)
        {
            TransactionStatus ts = new TransactionStatus();
            ts = _Provider.depositAccount(Convert.ToInt32(obj.AccountId), Convert.ToInt32(obj.amount));
            return ts;

           /* return new TransactionStatus()
            {
                Message = "users account has been credited",
                source_balance = 1000,
                destination_balance = 2000
            };*/
        }


        [HttpPost("withdraw")]
        public TransactionStatus withdraw([FromBody] dynamic obj)
        {

            TransactionStatus ts = new TransactionStatus();
            ts = _Provider.withdrawAccount(Convert.ToInt32(obj.AccountId),Convert.ToInt32(obj.amount));
            return ts;

            /*return new TransactionStatus()
            {
                Message = "users account has been debited",
                source_balance = 1000,
                destination_balance = 500
            };*/
        }


    }
}



