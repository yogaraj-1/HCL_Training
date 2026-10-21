
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class PaymentService
    {
        private readonly List<Enrollment> enrollments;
        private readonly List<Payment> payments;

        private int nextPaymentId = 1;

        public PaymentService(
            List<Enrollment> enrollments,
            List<Payment> payments)
        {
            this.enrollments = enrollments;
            this.payments = payments;
        }

        public void ProcessPayment(
            int enrollmentId,
            bool simulateSuccess)
        {
            Enrollment? enrollment = null;

            foreach (Enrollment item in enrollments)
            {
                if (item.EnrollmentId == enrollmentId)
                {
                    enrollment = item;
                    break;
                }
            }

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            if (enrollment.Status != EnrollmentStatus.Active)
            {
                Console.WriteLine(
                    "Payment is not allowed for an inactive enrollment.");
                return;
            }

            if (enrollment.IsPaid)
            {
                Console.WriteLine(
                    "This enrollment has already been paid.");
                return;
            }

            PaymentStatus status = simulateSuccess
                ? PaymentStatus.Successful
                : PaymentStatus.Failed;

            Payment payment = new Payment(
                nextPaymentId,
                enrollmentId,
                enrollment.FinalFee,
                status);

            payments.Add(payment);
            nextPaymentId++;

            if (status == PaymentStatus.Successful)
            {
                enrollment.IsPaid = true;
                Console.WriteLine("Payment successful!");
            }
            else
            {
                Console.WriteLine(
                    "Payment failed. You can try again.");
            }

            Console.WriteLine($"Payment ID : {payment.PaymentId}");
            Console.WriteLine($"Enrollment : {payment.EnrollmentId}");
            Console.WriteLine($"Amount     : {payment.Amount:C}");
            Console.WriteLine($"Status     : {payment.Status}");
        }

        public void DisplayPaymentHistory()
        {
            
            if (payments.Count == 0)
            {
                Console.WriteLine("No payment attempts recorded.");
                return;
            }

            Console.WriteLine("\n========== PAYMENT HISTORY ==========");

            foreach (Payment payment in payments)
            {
                payment.DisplayPayment();
            }
        }
    }
}
