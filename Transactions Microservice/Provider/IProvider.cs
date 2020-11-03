using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transactions_Microservice.Models;

namespace Transactions_Microservice.Provider
{
    public interface IProvider
    {
        void AddToTransactionHistory(TransactionHistory history);
        List<TransactionHistory> GetTransactionHistory(int CustomerId);
    }
}
