using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBanking.Models
{
    public class FundTransfer
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }

        public Decimal Amount { get; set; }
    }
}