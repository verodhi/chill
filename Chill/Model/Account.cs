using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chill.Model
{
    public class Account
    {
        public byte[] RowVersion { get; set; }
        public int AccountNumber { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
    }
}
