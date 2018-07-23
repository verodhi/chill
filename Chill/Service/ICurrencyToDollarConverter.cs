namespace Chill.Service
{
    public interface ICurrencyToDollarConverter
    {
        double ConvertToDollar(string currency, double amount);
    }
}