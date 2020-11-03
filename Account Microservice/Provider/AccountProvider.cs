using Account_Microservice.Models;
using Account_Microservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account_Microservice.Provider
{
    public class AccountProvider : IAccountProvider
    {
        IAccountRepository _Repository;
        public AccountProvider(IAccountRepository Repository)
        {
            _Repository = Repository;
        }

        public AccountCreationStatus AddAccount(int CustomerId, string AccountType)
        {
            AccountCreationStatus acs = new AccountCreationStatus();
          acs=  _Repository.AddAccount(CustomerId, AccountType);
            return acs;
        }

        public TransactionStatus depositAccount(int AccountId, int amount)
        {
           TransactionStatus ts = new TransactionStatus();
           ts= _Repository.depositAccount(AccountId, amount);
           return ts;
        }

        public IEnumerable<Account> getAllAccounts(int CustomerId)
        {
            List<Account> Listaccount = new List<Account>();
            Listaccount = _Repository.getAllAccounts(CustomerId).ToList();
            return Listaccount;
        }

        public Account getCustomerAccount(int AccountId)
        {
            Account a = new Account();
            a = _Repository.getCustomerAccount(AccountId);
            return a;
        }

        public IEnumerable<Statement> getStatement(int AccountId, DateTime from_date, DateTime to_date)
        {
            List<Statement> statements = new List<Statement>();
            statements = _Repository.getStatement(AccountId, from_date, to_date).ToList();
            return statements;
        }

        public TransactionStatus withdrawAccount(int AccountId, int amount)
        {
            TransactionStatus ts = new TransactionStatus();
            ts = _Repository.withdrawAccount(AccountId, amount);
            return ts;
        }
    }
}
