using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace EndPoint.Models
{
    public class Account
    {
        public int AccountNo { get; set; }
        public string Title { get; set; }
        public IEnumerable<int> CustomerIds { get; set; }
        public decimal Balance { get; set; }
    }

    public class AccountCreateRequest
    {
        public string firstName{ get; set; }
        public string lastName{ get; set; }
        public string contact{ get; set; }
        public string title{ get; set; }
        public decimal amount{ get; set; }
        public string notes{ get; set; }
    }

    public class AccountExternalTransactionRequest
    {
        public int accountNo { get; set; }
        public decimal amount { get; set; }
        public string notes { get; set; }
    }

    public class AccountInternalTransactionRequest
    {
        public int sourceAccountNo { get; set; }
        public int targetAccountNo { get; set; }
        public decimal amount { get; set; }
        public string notes { get; set; }
    }

}