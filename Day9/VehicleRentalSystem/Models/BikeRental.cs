
namespace VehicleRentalSystem.Models
{
    public class BikeRental : RentalContract
    {
        public BikeRental(
            int contractId,
            Renter renter,
            Vehicle vehicle,
            int rentalDays)
            : base(contractId, renter, vehicle, rentalDays)
        {
        }

        public override decimal CalculateRentalCost()
        {
            return Vehicle.DailyRate * RentalDays
                   + 50m * RentalDays;
        }

        public override void Display()
        {
            Console.WriteLine("\n--- BIKE RENTAL ---");
            base.Display();
            Console.WriteLine("Extra bike service: Rs.50 per day");
        }
    }
}
