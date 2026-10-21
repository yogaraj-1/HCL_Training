using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalSystem.Payments
{
    internal class DigitalPayment
    {
    }
}
namespace VehicleRentalSystem.Payments
{
    public class DigitalPayment : IPaymentProcessor
    {
        bool IPaymentProcessor.ProcessPayment(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine(
                    "Payment amount must be positive.");
                return false;
            }

            Console.WriteLine(
                $"Simulated digital payment: Rs.{amount:F2}");

            return true;
        }
    }
}
