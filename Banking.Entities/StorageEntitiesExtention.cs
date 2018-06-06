using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Entities
{
    public partial class StorageEntities
    {
        private int GetId(string sequence)
        {
            var ret = Database.SqlQuery<int>($"SELECT NEXT VALUE FOR {sequence}");
            return ret.FirstOrDefault();
        }

        public int CustomerId()
        {
            return GetId("customerseq");
        }

        public int AccountId()
        {
            return GetId("accountseq");
        }

        public int TransactionId()
        {
            return GetId("TransactionSeq");
        }
    }
}
