using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account_Microservice.Models;
using Account_Microservice.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountRepository _Repository;

        public AccountController(IAccountRepository Repository)
        {
            _Repository = Repository;
        }

        [HttpPost("createAccount")]
        public AccountCreationStatus createAccount(int CustomerId,string AccountType)
        {
            AccountCreationStatus acs = new AccountCreationStatus();
            acs = _Repository.AddAccount(CustomerId, AccountType);
            return acs;
        }

        [HttpGet("getCustomerAccounts")]
        public IEnumerable<Account> getCustomerAccounts(int CustomerId )
        {
            List<Account> accounts = new List<Account>();
            accounts = _Repository.getAllAccounts(CustomerId).ToList();
            return accounts;

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
            a = _Repository.getCustomerAccount(AccountId);
            return a;

            /*return new Account(){
                AccountId=1,
            CustomerId = 3,
            AccountType = "saving",
            Balance=1200
            };*/
        }


        [HttpGet("getAccountStatement")]
        public IEnumerable<Statement> getAccountStatement(int AccountId,DateTime from_date,DateTime to_date)
        {

            List<Statement> statements = new List<Statement>();
            statements = _Repository.getStatement(AccountId, from_date, to_date).ToList();
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
        public TransactionStatus deposit(int AccountId, int amount)
        {
            TransactionStatus ts = new TransactionStatus();
            ts = _Repository.depositAccount(AccountId, amount);
            return ts;

           /* return new TransactionStatus()
            {
                Message = "users account has been credited",
                source_balance = 1000,
                destination_balance = 2000
            };*/
        }


        [HttpPost("withdraw")]
        public TransactionStatus withdraw(int AccountId, int amount)
        {

            TransactionStatus ts = new TransactionStatus();
            ts = _Repository.withdrawAccount(AccountId, amount);
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



