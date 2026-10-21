
namespace VehicleRentalSystem.Models
{
    public class Renter
    {
        public int RenterId { get; private set; }
        public string Name { get; private set; }
        public string Contact { get; private set; }

        public Renter(int renterId, string name, string contact)
        {
            RenterId = renterId;
            Name = name;
            Contact = contact;
        }

        public void Display()
        {
            Console.WriteLine(
                $"Renter ID: {RenterId}, " +
                $"Name: {Name}, Contact: {Contact}");
        }
    }
}
