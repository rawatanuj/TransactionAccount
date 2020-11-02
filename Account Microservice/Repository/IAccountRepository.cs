using Account_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account_Microservice.Repository
{
  public interface IAccountRepository
    {

        AccountCreationStatus AddAccount(int CustomerId, string AccountType);
        IEnumerable<Account> getAllAccounts(int CustomerId);
        Account getCustomerAccount(int AccountId);
      IEnumerable<Statement>  getStatement(int AccountId, DateTime from_date, DateTime to_date);
       TransactionStatus depositAccount(int AccountId, float amount);
        TransactionStatus withdrawAccount(int AccountId, float amount);

    }
}
