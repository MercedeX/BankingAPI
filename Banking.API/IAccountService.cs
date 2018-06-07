using EndPoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPoint
{
   public interface IAccountService
    {
        int CreateAccount(string firstName, string lastName, string contact, string title, decimal amount, string notes);
        bool CloseAccount(int accountNo);

        bool ExternalTransfer(int dest, decimal amount, string notes);
        bool InternalTransfer(int sourceAcc, int destAcc, decimal amount, string notes);

        IEnumerable<Account> GetAccounts(int customerId);
        IEnumerable<Transaction> GetTransactions(int accountId, DateTime startDate, DateTime endDate);
    }
}
