
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class ReportService
    {
        private readonly Course[] courses;
        private readonly List<Enrollment> enrollments;
        private readonly List<Payment> payments;

        public ReportService(
            Course[] courses,
            List<Enrollment> enrollments,
            List<Payment> payments)
        {
            this.courses = courses;
            this.enrollments = enrollments;
            this.payments = payments;
        }

        // Report A: Course occupancy
        public void DisplayCourseOccupancy()
        {
            Console.WriteLine("\n========== COURSE OCCUPANCY ==========");

            foreach (Course course in courses)
            {
                int availableSeats =
                    course.TotalSeats - course.EnrolledCount;

                double occupancy = course.TotalSeats > 0
                    ? (double)course.EnrolledCount
                        / course.TotalSeats * 100
                    : 0;

                Console.WriteLine($"\nCourse: {course.Title}");
                Console.WriteLine($"Total seats     : {course.TotalSeats}");
                Console.WriteLine($"Occupied seats  : {course.EnrolledCount}");
                Console.WriteLine($"Available seats : {availableSeats}");
                Console.WriteLine($"Occupancy       : {occupancy:F2}%");
            }
        }

        // Report B: Expected fees from active enrollments
        public void DisplayExpectedFees()
        {
            decimal expectedFees = 0;

            foreach (Enrollment enrollment in enrollments)
            {
                if (enrollment.Status == EnrollmentStatus.Active)
                {
                    expectedFees += enrollment.FinalFee;
                }
            }

            Console.WriteLine("\n========== EXPECTED FEES ==========");
            Console.WriteLine(
                $"Expected fees from active enrollments: {expectedFees:C}");
        }

        // Report C: Payment summary
        public void DisplayPaymentSummary()
        {
            int successfulCount = 0;
            int failedCount = 0;
            decimal totalCollected = 0;

            foreach (Payment payment in payments)
            {
                if (payment.Status == PaymentStatus.Successful)
                {
                    successfulCount++;
                    totalCollected += payment.Amount;
                }
                else if (payment.Status == PaymentStatus.Failed)
                {
                    failedCount++;
                }
            }

            Console.WriteLine("\n========== PAYMENT SUMMARY ==========");
            Console.WriteLine($"Successful attempts : {successfulCount}");
            Console.WriteLine($"Failed attempts     : {failedCount}");
            Console.WriteLine($"Total collected     : {totalCollected:C}");
        }

        // Report D: Course enrollment summary
        public void DisplayCourseEnrollmentSummary()
        {
            Console.WriteLine("\n====== COURSE ENROLLMENT SUMMARY ======");

            foreach (Course course in courses)
            {
                int activeCount = 0;

                foreach (Enrollment enrollment in enrollments)
                {
                    if (enrollment.CourseCode == course.CourseCode &&
                        enrollment.Status == EnrollmentStatus.Active)
                    {
                        activeCount++;
                    }
                }

                Console.WriteLine($"\nCourse: {course.Title}");
                Console.WriteLine($"Active enrollments: {activeCount}");

                if (course.TotalSeats - activeCount <= 0)
                {
                    Console.WriteLine("Status: COURSE FULL");
                }
                else
                {
                    Console.WriteLine(
                        $"Available seats: {course.TotalSeats - activeCount}");
                }
            }
        }

        // Display all four reports
        public void DisplayAllReports()
        {
            DisplayCourseOccupancy();
            DisplayExpectedFees();
            DisplayPaymentSummary();
            DisplayCourseEnrollmentSummary();
        }
    }
}
