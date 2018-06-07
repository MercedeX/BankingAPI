using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndPoint.Models
{
    public enum TransactionType
    {
        Credit = 0, Debit =1
    }

    public class Transaction
    {
        public int? From { get; set; }
        public int? To { get; set; }
        public decimal Amount { get; set; }
        public TransactionType FlowType { get; set; }
        public DateTime? PostedOn { get; internal set; }
    }
}