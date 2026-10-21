
namespace VehicleRentalSystem.Models
{
    public abstract class RentalContract
    {
        public int ContractId { get; set; }
        public Renter Renter { get; set; }
        public Vehicle Vehicle { get; set; }
        public int RentalDays { get; set; }
        public decimal RentalCost { get; protected set; }
        public decimal Deposit { get; protected set; }
        public bool IsReturned { get; set; }
        public bool IsPaid { get; set; }

        protected RentalContract(
            int contractId,
            Renter renter,
            Vehicle vehicle,
            int rentalDays)
        {
            ContractId = contractId;
            Renter = renter;
            Vehicle = vehicle;
            RentalDays = rentalDays;
            IsReturned = false;
            IsPaid = false;

            RentalCost = CalculateRentalCost();
            Deposit = RentalCost * 0.20m;
        }

        public abstract decimal CalculateRentalCost();

        public virtual void Display()
        {
            Console.WriteLine("\n--- Rental Contract ---");
            Console.WriteLine($"Contract ID : {ContractId}");
            Console.WriteLine($"Renter      : {Renter.Name}");
            Console.WriteLine($"Vehicle     : {Vehicle.Model}");
            Console.WriteLine($"Category    : {Vehicle.Category}");
            Console.WriteLine($"Days        : {RentalDays}");
            Console.WriteLine($"Rental Cost : Rs.{RentalCost:F2}");
            Console.WriteLine($"Deposit     : Rs.{Deposit:F2}");
            Console.WriteLine($"Paid        : {(IsPaid ? "Yes" : "No")}");
            Console.WriteLine($"Status      : {(IsReturned ? "Returned" : "Active")}");
        }
    }
}
