using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transactions_Microservice.Models;
using Transactions_Microservice.Repository;

namespace Transactions_Microservice.Provider
{
    public class TransactionProvider : IProvider
    {
        private IRepository _repo;
        public TransactionProvider(IRepository repo)
        {
            _repo = repo;
        }
        public void AddToTransactionHistory(TransactionHistory history)
        {
            _repo.AddToTransactionHistory(history);
        }

        public List<TransactionHistory> GetTransactionHistory(int CustomerId)
        {
            return _repo.GetTransactionHistory(CustomerId);
        }
    }
}
