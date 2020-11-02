using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Account_Microservice.Models
{
    public class Statement
    {
        [Key]
        public int StatementId { get; set; }
        public int AccountId { get; set; }
        public DateTime date { get; set; }
        public string refno {get;set;}
        public DateTime ValueDate { get; set; }
        public float Withdrawal { get; set; }
        public float Deposit { get; set; }
        public float ClosingBalance { get; set;}
    }
}
