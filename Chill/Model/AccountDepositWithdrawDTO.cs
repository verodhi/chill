using Chill.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chill.Model
{
    public class AccountDepositWithdrawDTO
    {
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        [CurrencyValidator(AllowCurrency = "Thai Bhat,United States Dollar,Euro")]
        public string Currency { get; set; }
    }
}
