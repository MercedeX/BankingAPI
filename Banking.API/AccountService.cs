using Banking.Entities;
using EndPoint.Models;
using NLog;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain = Banking.Entities;
using Model = EndPoint.Models;

namespace EndPoint
{
    public class AccountService : IAccountService
    {
        readonly StorageEntities _context;
        readonly ILogger _log;

        public AccountService(ILogger log)
        {
            Raise.ArgumentNullException.IfIsNull<ILogger>(log);

            _context = new StorageEntities();
            _log = log;
        }

        bool AccountExists(int accountNo) => _context.Accounts.Any(x => x.AccountNo == accountNo && (x.IsActive ?? false));
        bool CustomerExists(int customerId) => _context.Customers.Any(x => x.CustomerId == customerId && (x.IsActive ?? false));

        public int CreateAccount(string firstName, string lastName, string contact, string title, decimal amount, string notes)
        {
            var ret = 0;


            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(title))
                throw new ApplicationException("Account title or customer first name could not be empty");

            var customer = new Domain.Customer { FirstName = firstName, LastName = lastName, ContactInfo = contact, CustomerId = _context.CustomerId(), IsActive = true };
            var account = new Domain.Account { AccountName = title, AccountNo = _context.AccountId(), Balance = 0.0M, IsActive = true };
            customer.Accounts.Add(account);

            _context.SaveChanges();
            ret = account.AccountNo;
            _log.Info($"Account {title} for {firstName} {lastName} has been created successfully");

            ExternalTransfer(account.AccountNo, amount, "Initial Deposit");


            return ret;
        }
        public bool CloseAccount(int accountNo)
        {
            var ret = false;
            Raise.ArgumentException.IfNot(AccountExists(accountNo), "", "Account does not exist");


            var account = _context.Accounts.FirstOrDefault(x => x.AccountNo == accountNo);
            account.IsActive = false;
            _context.SaveChanges();
            ret = true;



            return ret;

        }

        public bool ExternalTransfer(int dest, decimal amount, string notes)
        {
            var ret = false;
            Raise.ArgumentException.IfNot(AccountExists(dest), "", "Invalid account mentioned");
            Raise.ArgumentException.If(amount == 0.0M, "", "Amount cannot be empty");

            var account = _context.Accounts.FirstOrDefault(x => x.AccountNo == dest);
            if (amount < 0.0M && Math.Abs(amount) <= account.Balance)
            {
                //Withdrawn
                var trx = new Domain.Transaction
                {
                    TransactionId = _context.TransactionId(),
                    SourceNo = dest,
                    AccountSource = account,
                    TargetNo = null,
                    AccountTarget = null,
                    Amount = amount,
                    PostedOn = DateTime.Now,
                    Notes = notes
                };
                account.Balance -= amount;
                _context.Transactions.Add(trx);
            }
            else
            {
                //Deposit
                var trx = new Domain.Transaction
                {
                    TransactionId = _context.TransactionId(),
                    TargetNo = dest,
                    AccountTarget = account,
                    SourceNo = null,
                    AccountSource = null,
                    Amount = amount,
                    PostedOn = DateTime.Now,
                    Notes = notes
                };
                account.Balance += amount;
                _context.Transactions.Add(trx);
            }

            _context.SaveChanges();
            ret = true;

            _log.Info($"{(amount > 0.0M ? "debit" : "credit")} has been made into {account.AccountName}: {account.AccountNo}");

            return ret;
        }
        public bool InternalTransfer(int sourceAcc, int destAcc, decimal amount, string notes)
        {
            var ret = false;

            Raise.ArgumentException.If(amount <= 0, "", $"Empty transaction cannot be performed, amount is {amount}");
            Raise.ArgumentException.IfNot(AccountExists(sourceAcc), "", $"Source account {sourceAcc} doe snot exist");
            Raise.ArgumentException.IfNot(AccountExists(destAcc), $"Destination account { destAcc} does not exist");



            var sourceAccount = _context.Accounts.FirstOrDefault(x => x.AccountNo == sourceAcc);
            var targetAccount = _context.Accounts.FirstOrDefault(x => x.AccountNo == destAcc);

            Raise.ArgumentException.If(amount > sourceAccount.Balance, "", $"Insufficient funds in the account {sourceAcc}");


            var trx = new Domain.Transaction
            {
                TransactionId = _context.TransactionId(),
                SourceNo = sourceAcc,
                TargetNo = destAcc,
                AccountSource = sourceAccount,
                AccountTarget = targetAccount,
                Amount = amount,
                PostedOn = DateTime.Now,
                Notes = ""
            };

            sourceAccount.Balance -= amount;
            targetAccount.Balance += amount;

            _context.Transactions.Add(trx);
            _context.SaveChanges();
            ret = true;

            _log.Info($"${amount} has been moved from {sourceAcc} to {destAcc} successfully");

            return ret;
        }

        public IEnumerable<Model.Account> GetAccounts(int customerId)
        {
            var ret = new List<Model.Account>();

            Raise.ArgumentException.IfNot(CustomerExists(customerId), "", "Customer does not exist");

            ret = _context.Customers.Where(x => x.CustomerId == customerId)
            .SelectMany(x => x.Accounts)
            .Select(x => new Model.Account
            {
                AccountNo = x.AccountNo,
                Balance = x.Balance ?? 0.0M,
                CustomerIds = x.Customers.Select(y => y.CustomerId),
                Title = x.AccountName
            })
            .ToList();

            return ret.AsEnumerable();
        }
        public IEnumerable<Model.Transaction> GetTransactions(int accountId, DateTime startDate, DateTime endDate)
        {
            var ret = new List<Model.Transaction>().AsEnumerable();

            Raise.ArgumentException.IfNot(AccountExists(accountId), "", "Account does not exist");

            var account = _context.Accounts.FirstOrDefault(x => x.AccountNo == accountId);
            ret = account.TransactionsSource.Concat(account.TransactionsTarget)
            .Select(x => new Model.Transaction
            {
                From = x.AccountSource?.AccountNo,
                To = x.AccountTarget?.AccountNo,
                PostedOn = x.PostedOn,
                Amount = Math.Abs(x.Amount),
                FlowType = x.AccountTarget.AccountNo == accountId ? TransactionType.Debit : TransactionType.Credit
            })
            .OrderByDescending(x => x.PostedOn);


            return ret;
        }
    }
}