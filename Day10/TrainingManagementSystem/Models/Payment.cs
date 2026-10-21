
namespace TrainingManagementSystem.Models
{
    public enum PaymentStatus
    {
        Pending,
        Successful,
        Failed
    }

    public class Payment
    {
        public int PaymentId { get; set; }
        public int EnrollmentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }

        public Payment(
            int paymentId,
            int enrollmentId,
            decimal amount,
            PaymentStatus status)
        {
            PaymentId = paymentId;
            EnrollmentId = enrollmentId;
            Amount = amount;
            PaymentDate = DateTime.Now;
            Status = status;
        }

        public void DisplayPayment()
        {
            Console.WriteLine(
                "{0,-12} {1,-15} {2,-15:C} {3,-25} {4,-12}",
                PaymentId,
                EnrollmentId,
                Amount,
                PaymentDate,
                Status);
        }
    }
}
