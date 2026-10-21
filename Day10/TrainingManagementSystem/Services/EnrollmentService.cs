
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class EnrollmentService
    {
        private readonly Dictionary<string, Learner> learners;
        private readonly Course[] courses;
        private readonly List<Enrollment> enrollments;
        private readonly FeeCalculator feeCalculator;

        private int nextEnrollmentId = 1;

        public EnrollmentService(
            Dictionary<string, Learner> learners,
            Course[] courses,
            List<Enrollment> enrollments,
            FeeCalculator feeCalculator)
        {
            this.learners = learners;
            this.courses = courses;
            this.enrollments = enrollments;
            this.feeCalculator = feeCalculator;
        }

        public bool EnrollLearner(
            string learnerId,
            string courseCode,
            bool isOnline,
            bool isEarlyBird,
            int groupSize)
        {
            if (!learners.ContainsKey(learnerId))
            {
                Console.WriteLine("Learner not found.");
                return false;
            }

            Course? selectedCourse = null;

            foreach (Course course in courses)
            {
                if (string.Equals(
                    course.CourseCode,
                    courseCode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    selectedCourse = course;
                    break;
                }
            }

            if (selectedCourse == null)
            {
                Console.WriteLine("Course not found.");
                return false;
            }

            foreach (Enrollment enrollment in enrollments)
            {
                if (enrollment.LearnerId == learnerId &&
                    string.Equals(
                        enrollment.CourseCode,
                        selectedCourse.CourseCode,
                        StringComparison.OrdinalIgnoreCase) &&
                    enrollment.Status == EnrollmentStatus.Active)
                {
                    Console.WriteLine(
                        "Learner is already actively enrolled in this course.");
                    return false;
                }
            }

            int availableSeats =
                selectedCourse.TotalSeats - selectedCourse.EnrolledCount;

            if (availableSeats <= 0)
            {
                Console.WriteLine("Sorry, this course is full.");
                return false;
            }

            var fee = feeCalculator.CalculateFee(
                selectedCourse.Fee,
                isOnline,
                isEarlyBird,
                groupSize);

            Enrollment newEnrollment = new Enrollment(
                nextEnrollmentId,
                learnerId,
                selectedCourse.CourseCode,
                fee.BaseFee,
                fee.DiscountAmount,
                fee.FinalFee);

            enrollments.Add(newEnrollment);
            selectedCourse.EnrolledCount++;
            nextEnrollmentId++;

            Console.WriteLine("\nEnrollment successful!");
            Console.WriteLine($"Enrollment ID : {newEnrollment.EnrollmentId}");
            Console.WriteLine($"Learner       : {learners[learnerId].Name}");
            Console.WriteLine($"Course        : {selectedCourse.Title}");
            Console.WriteLine($"Final fee     : {newEnrollment.FinalFee:C}");
            Console.WriteLine($"Available seats: {selectedCourse.TotalSeats - selectedCourse.EnrolledCount}");

            return true;
        }

        public bool CancelEnrollment(int enrollmentId)
        {
            Enrollment? selectedEnrollment = null;

            foreach (Enrollment enrollment in enrollments)
            {
                if (enrollment.EnrollmentId == enrollmentId)
                {
                    selectedEnrollment = enrollment;
                    break;
                }
            }

            if (selectedEnrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return false;
            }

            if (selectedEnrollment.Status != EnrollmentStatus.Active)
            {
                Console.WriteLine(
                    "Only active enrollments can be cancelled.");
                return false;
            }

            selectedEnrollment.Status = EnrollmentStatus.Cancelled;

            foreach (Course course in courses)
            {
                if (string.Equals(
                    course.CourseCode,
                    selectedEnrollment.CourseCode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    if (course.EnrolledCount > 0)
                    {
                        course.EnrolledCount--;
                    }

                    break;
                }
            }

            Console.WriteLine(
                $"Enrollment {enrollmentId} cancelled successfully.");

            return true;
        }

        public void DisplayEnrollments()
        {
            if (enrollments.Count == 0)
            {
                Console.WriteLine("No enrollment records found.");
                return;
            }

            Console.WriteLine(
                "\n{0,-12} {1,-15} {2,-12} {3,-15} {4,-15} {5,-12}",
                "Enroll ID",
                "Learner ID",
                "Course",
                "Base Fee",
                "Final Fee",
                "Status");

            Console.WriteLine(new string('-', 85));

            foreach (Enrollment enrollment in enrollments)
            {
                enrollment.DisplayEnrollment();
            }
        }
    }
}
