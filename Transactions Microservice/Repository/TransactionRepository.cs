using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transactions_Microservice.Models;

namespace Transactions_Microservice.Repository
{
    public class TransactionRepository : IRepository
    {
        static List<TransactionHistory> status = new List<TransactionHistory>();
        public void AddToTransactionHistory(TransactionHistory history)
        {
            status.Add(history);
        }

        public void GetTransactionHistory(int CustomerId)
        {
            throw new NotImplementedException();
        }
    }
}
