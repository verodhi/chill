using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chill.Service
{
    public sealed class CurrencyValidator: ValidationAttribute
    {
        public string AllowCurrency { get; set; }

        protected override ValidationResult IsValid (object currency, ValidationContext validationContext)
        {
            string[] myarr = AllowCurrency.ToString().Split(',');
            if (myarr.Contains(currency))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Currently we only support Thai Bhat, Euro and United States Dollars");
            }
        }
    }
}
