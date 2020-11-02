using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account_Microservice.Models
{
    public class TransactionStatus
    {
        public string Message { get; set; }

        public float source_balance { get; set; }

        public float destination_balance { get; set; }
    }
}
