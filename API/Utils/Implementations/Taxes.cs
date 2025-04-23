namespace API.Utils.Implementations
{
    public class Taxes
    {
        public static double GetTaxes()
        {
            const double taxRate = 0.21; // 21% tax rate
            return taxRate;
        }

        public static double GetIntervalInterests()
        {
            const double interestRate = 0.05; // 5% interest rate
            return interestRate;
        }

        public static double GetReferalCodeDiscount()
        {
            const double discountRate = 0.10; // 10% discount rate
            return discountRate;
        }
    }
}
