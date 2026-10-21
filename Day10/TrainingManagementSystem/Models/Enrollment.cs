
namespace TrainingManagementSystem.Models
{
    public enum EnrollmentStatus
    {
        Active,
        Cancelled,
        Completed
    }

    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public string LearnerId { get; set; }
        public string CourseCode { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public decimal BaseFee { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalFee { get; set; }


        public bool IsPaid { get; set; } = false;

        public EnrollmentStatus Status { get; set; }

        public Enrollment(
            int enrollmentId,
            string learnerId,
            string courseCode,
            decimal baseFee,
            decimal discountAmount,
            decimal finalFee)
        {
            EnrollmentId = enrollmentId;
            LearnerId = learnerId;
            CourseCode = courseCode;
            EnrollmentDate = DateTime.Now;

            BaseFee = baseFee;
            DiscountAmount = discountAmount;
            FinalFee = finalFee;

            Status = EnrollmentStatus.Active;
        }

        public void DisplayEnrollment()
        {
            Console.WriteLine(
                "{0,-12} {1,-15} {2,-12} {3,-15:C} {4,-15:C} {5,-12}",
                EnrollmentId,
                LearnerId,
                CourseCode,
                BaseFee,
                FinalFee,
                Status);
        }
    }
}
