
using VehicleRentalSystem.Models;
using VehicleRentalSystem.Payments;
namespace VehicleRentalSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vehicle[] vehicles =
            {
                new Vehicle("TN58AB1234", "Swift", "Car", 1800m),
                new Vehicle("TN58CD5678", "Baleno", "Car", 2200m),
                new Vehicle("TN58EF9012", "Activa", "Bike", 500m),
                new Vehicle("TN58GH3456", "Royal Enfield", "Bike", 900m),
                new Vehicle("TN58IJ7890", "Creta", "Car", 3000m)
            };
            Renter[] renters = new Renter[20];
            int renterCount = 0;
            int nextRenterId = 1001;
            RentalContract[] rentals = new RentalContract[20];
            int rentalCount = 0;
            int nextContractId = 5001;

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n==================================");
                Console.WriteLine("      VEHICLE RENTAL SYSTEM");
                Console.WriteLine("==================================");
                Console.WriteLine("1. View All Vehicles");
                Console.WriteLine("2. Search Vehicle");
                Console.WriteLine("3. Sort Vehicles by Daily Rate");
                Console.WriteLine("4. Register Renter");
                Console.WriteLine("5. Rent Vehicle");
                Console.WriteLine("6. View Rental Contracts");
                Console.WriteLine("8. Return Vehicle");
                Console.WriteLine("9. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {
                    case "1":
                        DisplayVehicles(vehicles);
                        break;

                    case "2":
                        SearchVehicle(vehicles);
                        break;

                    case "3":
                        SortVehicles(vehicles);
                        break;

                    case "4":
                        RegisterRenter(
                            renters, ref renterCount, ref nextRenterId);
                        break;

                    case "5":
                        RentVehicle(
                            vehicles, renters, rentals,
                            renterCount, ref rentalCount,
                            ref nextContractId);
                        break;

                    case "6":
                        ViewRentals(rentals, rentalCount);
                        break;

                    case "7":
                        ProcessRentalPayment(rentals, rentalCount);
                        break;

                    case "8":
                        ReturnVehicle(rentals, rentalCount);
                        break;

                    case "9":
                        running = false;
                        Console.WriteLine("Thank you for using the system!");
                        break;
                    

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void DisplayVehicles(Vehicle[] vehicles)
        {
            Console.WriteLine("\n--- Available Vehicles ---");

            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.Display();
            }
        }

        static void SearchVehicle(Vehicle[] vehicles)
        {
            Console.Write("\nEnter model or category: ");
            string search = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(search))
            {
                Console.WriteLine("Search cannot be empty.");
                return;
            }

            bool found = false;

            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.Model.Contains(
                        search, StringComparison.OrdinalIgnoreCase)
                    || vehicle.Category.Equals(
                        search, StringComparison.OrdinalIgnoreCase))
                {
                    vehicle.Display();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No matching vehicle found.");
            }
        }

        static void SortVehicles(Vehicle[] vehicles)
        {
            Array.Sort(vehicles, (a, b) =>
                a.DailyRate.CompareTo(b.DailyRate));

            Console.WriteLine("\nVehicles sorted by daily rental rate:");
            DisplayVehicles(vehicles);
        }


        static void RegisterRenter(
            Renter[] renters,
            ref int renterCount,
            ref int nextRenterId)
        {
            if (renterCount >= renters.Length)
            {
                Console.WriteLine("Renter storage is full.");
                return;
            }

            Console.Write("Enter renter name: ");
            string name = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            Console.Write("Enter contact number: ");
            string contact = Console.ReadLine()?.Trim() ?? "";

            if (contact.Length < 10 ||
                !contact.All(char.IsDigit))
            {
                Console.WriteLine("Enter a valid contact number.");
                return;
            }

            Renter renter = new Renter(
                nextRenterId, name, contact);

            renters[renterCount] = renter;
            renterCount++;
            nextRenterId++;

            Console.WriteLine("\nRenter registered successfully!");
            renter.Display();
        }

        static void RentVehicle(
            Vehicle[] vehicles,
            Renter[] renters,
            RentalContract[] rentals,
            int renterCount,
            ref int rentalCount,
            ref int nextContractId)
        {
            if (rentalCount >= rentals.Length)
            {
                Console.WriteLine("Rental storage is full.");
                return;
            }

            Console.Write("Enter renter ID: ");

            if (!int.TryParse(Console.ReadLine(), out int renterId))
            {
                Console.WriteLine("Invalid renter ID.");
                return;
            }

            Renter? selectedRenter = null;

            for (int i = 0; i < renterCount; i++)
            {
                if (renters[i]?.RenterId == renterId)
                {
                    selectedRenter = renters[i];
                    break;
                }
            }

            if (selectedRenter == null)
            {
                Console.WriteLine("Renter not found. Register first.");
                return;
            }

            Console.Write("Enter vehicle registration number: ");
            string registration =
                Console.ReadLine()?.Trim() ?? "";

            int vehicleIndex = -1;

            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i].RegistrationNumber.Equals(
                    registration, StringComparison.OrdinalIgnoreCase))
                {
                    vehicleIndex = i;
                    break;
                }
            }

            if (vehicleIndex == -1)
            {
                Console.WriteLine("Vehicle not found.");
                return;
            }

            Vehicle selectedVehicle = vehicles[vehicleIndex];

            if (!selectedVehicle.IsAvailable)
            {
                Console.WriteLine("Vehicle is already rented.");
                return;
            }

            Console.Write("Enter rental days: ");

            if (!int.TryParse(Console.ReadLine(), out int days)
                || days <= 0)
            {
                Console.WriteLine("Rental days must be positive.");
                return;
            }

            RentalContract contract = new RentalContract(
                nextContractId,
                selectedRenter,
                selectedVehicle,
                days);

            rentals[rentalCount] = contract;
            rentalCount++;
            nextContractId++;

            selectedVehicle.IsAvailable = false;

            Console.WriteLine("\nVehicle rented successfully!");
            contract.Display();
        }

        static void ViewRentals(
            RentalContract[] rentals, int rentalCount)
        {
            if (rentalCount == 0)
            {
                Console.WriteLine("No rental contracts found.");
                return;
            }

            for (int i = 0; i < rentalCount; i++)
            {
                rentals[i].Display();
            }
        }


        static void ReturnVehicle(
            RentalContract[] rentals,
            int rentalCount)
        {
            Console.Write("Enter Contract ID: ");

            if (!int.TryParse(Console.ReadLine(), out int contractId))
            {
                Console.WriteLine("Invalid contract ID.");
                return;
            }

            RentalContract? selectedContract = null;

            foreach (RentalContract? rental in rentals)
            {
                if (rental != null &&
                    rental.ContractId == contractId)
                {
                    selectedContract = rental;
                    break;
                }
            }

            if (selectedContract == null)
            {
                Console.WriteLine("Contract not found.");
                return;
            }

            if (selectedContract.IsReturned)
            {
                Console.WriteLine("Vehicle has already been returned.");
                return;
            }

            Console.Write("Enter extra days (0 if on time): ");

            if (!int.TryParse(Console.ReadLine(), out int extraDays)
                || extraDays < 0)
            {
                Console.WriteLine("Invalid number of extra days.");
                return;
            }

            Console.Write("Is there any damage? (yes/no): ");
            string damageInput =
                Console.ReadLine()?.Trim() ?? "";

            if (!damageInput.Equals("yes",
                    StringComparison.OrdinalIgnoreCase) &&
                !damageInput.Equals("no",
                    StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Enter yes or no.");
                return;
            }

            decimal extraCharge =
                extraDays * selectedContract.Vehicle.DailyRate;

            const decimal damageFee = 1500m;

            decimal damageCharge =
                damageInput.Equals("yes",
                    StringComparison.OrdinalIgnoreCase)
                    ? damageFee
                    : 0m;

            decimal totalCharge =
                selectedContract.RentalCost
                + extraCharge
                + damageCharge;

            decimal balanceDue =
                totalCharge - selectedContract.Deposit;

            Console.WriteLine("\n========== RETURN BILL ==========");
            Console.WriteLine(
                $"Contract ID       : {selectedContract.ContractId}");
            Console.WriteLine(
                $"Vehicle           : {selectedContract.Vehicle.Model}");
            Console.WriteLine(
                $"Original rental   : Rs.{selectedContract.RentalCost:F2}");
            Console.WriteLine(
                $"Extra-day charges : Rs.{extraCharge:F2}");
            Console.WriteLine(
                $"Damage charges    : Rs.{damageCharge:F2}");
            Console.WriteLine(
                $"Total charges     : Rs.{totalCharge:F2}");
            Console.WriteLine(
                $"Deposit credited  : Rs.{selectedContract.Deposit:F2}");
            Console.WriteLine(
                $"Balance due       : Rs.{balanceDue:F2}");
            Console.WriteLine("=================================");

            selectedContract.IsReturned = true;
            selectedContract.Vehicle.IsAvailable = true;

            Console.WriteLine("Vehicle returned successfully!");
        }
        static void ProcessRentalPayment(
            RentalContract[] rentals,
            int rentalCount)
        {
            Console.Write("Enter Contract ID: ");

            if (!int.TryParse(Console.ReadLine(), out int contractId))
            {
                Console.WriteLine("Invalid contract ID.");
                return;
            }

            RentalContract? contract = null;

            for (int i = 0; i < rentalCount; i++)
            {
                if (rentals[i] != null &&
                    rentals[i].ContractId == contractId)
                {
                    contract = rentals[i];
                    break;
                }
            }

            if (contract == null)
            {
                Console.WriteLine("Contract not found.");
                return;
            }

            if (contract.IsPaid)
            {
                Console.WriteLine("Payment already completed.");
                return;
            }

            Console.WriteLine($"Amount: Rs.{contract.RentalCost:F2}");
            Console.WriteLine("1. Cash Payment");
            Console.WriteLine("2. Digital Payment");
            Console.Write("Choose payment method: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            IPaymentProcessor? processor = choice switch
            {
                "1" => new CashPayment(),
                "2" => new DigitalPayment(),
                _ => null
            };

            if (processor == null)
            {
                Console.WriteLine("Invalid payment method.");
                return;
            }

            bool success = processor.ProcessPayment(contract.RentalCost);

            if (success)
            {
                contract.IsPaid = true;
                Console.WriteLine("Payment completed successfully!");
            }
            else
            {
                Console.WriteLine("Payment failed.");
            }
        }

    }
}
