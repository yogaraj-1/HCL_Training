
namespace TrainingManagementSystem.Services
{
    public class FeeCalculator
    {
        private const decimal EarlyBirdRate = 0.10m;
        private const decimal GroupDiscountRate = 0.05m;

        private const decimal OnlineSurcharge = 200m;
        private const decimal ClassroomSurcharge = 800m;

        // Calculates the fee without discounts.
        public (decimal BaseFee, decimal DiscountAmount, decimal FinalFee)
            CalculateFee(decimal baseFee, bool isOnline)
        {
            ValidateFee(baseFee);

            decimal surcharge = isOnline
                ? OnlineSurcharge
                : ClassroomSurcharge;

            decimal finalFee = baseFee + surcharge;

            return (baseFee, 0m, finalFee);
        }

        // Calculates the fee with optional discounts.
        public (decimal BaseFee, decimal DiscountAmount, decimal FinalFee)
            CalculateFee(
                decimal baseFee,
                bool isOnline,
                bool isEarlyBird,
                int groupSize)
        {
            ValidateFee(baseFee);

            if (groupSize < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(groupSize),
                    "Group size must be at least 1.");
            }

            decimal discount = 0m;

            if (isEarlyBird)
            {
                discount += baseFee * EarlyBirdRate;
            }

            if (groupSize >= 3)
            {
                discount += baseFee * GroupDiscountRate;
            }

            decimal surcharge = isOnline
                ? OnlineSurcharge
                : ClassroomSurcharge;

            decimal finalFee = baseFee - discount + surcharge;

            return (baseFee, discount, finalFee);
        }

        private void ValidateFee(decimal baseFee)
        {
            if (baseFee < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(baseFee),
                    "Course fee cannot be negative.");
            }
        }

        public void DisplayFeeBreakdown(
            decimal baseFee,
            decimal discount,
            decimal finalFee,
            bool isOnline)
        {
            Console.WriteLine("\n========== FEE BREAKDOWN ==========");
            Console.WriteLine($"Base fee       : {baseFee:C}");
            Console.WriteLine($"Discount       : {discount:C}");
            Console.WriteLine(
                $"Session charge : {(isOnline ? OnlineSurcharge : ClassroomSurcharge):C}");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Final fee      : {finalFee:C}");
            Console.WriteLine("===================================");
        }
    }
}
