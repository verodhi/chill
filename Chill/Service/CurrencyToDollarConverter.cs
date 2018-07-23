using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chill.Service
{
    public class CurrencyToDollarConverter : ICurrencyToDollarConverter
    {
        // The currency conversion is hard coded here supports thai bhat dollar and euro.
        // The ideal way will be to use the API provided by trustable source.
        // There are free api providers but for the sake of simplicity the conversion rate is hard coded.
        public double ConvertToDollar(string currency, double amount)
        {
            switch (currency)
            {
                case "Thai Bhat":
                    return amount * 0.030;
                    break;
                case "United States Dollar":
                    return amount;
                    break;
                case "Euro":
                    return amount * 1.17;
                    break;
                default:
                    return 0.0;
            }
        }
    }
}
