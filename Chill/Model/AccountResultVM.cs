using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chill.Model
{
    public class AccountResultVM
    {
        public AccountResultVM(int accountNumber, bool success, double balance, string currency, string message)
        {
            AccountNumber = accountNumber;
            Successful = success;
            Balance = balance;
            Currency = currency;
            Message = message;
        }
        public AccountResultVM()
        {
        }
        public int AccountNumber { get; set; }
        public bool Successful { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public string Message { get; set; }
    }
}
