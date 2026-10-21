
namespace VehicleRentalSystem.Payments
{
    public class CashPayment : IPaymentProcessor
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine(
                $"Cash payment of Rs.{amount:F2} received.");

            return amount >= 0;
        }
    }
}
