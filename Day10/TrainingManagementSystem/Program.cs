
using TrainingManagementSystem.Models;
using TrainingManagementSystem.Services;

namespace TrainingManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Shared application data
            Course[] courses =
            {
                new Course("C101", "C# Programming", 5000m, 15, 30),
                new Course("C102", "Java Programming", 6000m, 20, 25),
                new Course("C103", "Python Programming", 4500m, 12, 30),
                new Course("C104", "Web Development", 7000m, 25, 20),
                new Course("C105", "SQL Database", 3500m, 10, 25)
            };

            Dictionary<string, Learner> learners =
                new Dictionary<string, Learner>(
                    StringComparer.OrdinalIgnoreCase);

            List<Enrollment> enrollments = new List<Enrollment>();
            List<Payment> payments = new List<Payment>();

            // Initialize services with shared data
            FeeCalculator feeCalculator = new FeeCalculator();

            TrainingService trainingService =
                new TrainingService(courses);

            LearnerService learnerService =
                new LearnerService(learners);

            EnrollmentService enrollmentService =
                new EnrollmentService(
                    learners, courses, enrollments, feeCalculator);

            PaymentService paymentService =
                new PaymentService(enrollments, payments);

            ReportService reportService =
                new ReportService(courses, enrollments, payments);

            bool running = true;

            Console.WriteLine(
                "==============================================");
            Console.WriteLine(
                "       TRAINING MANAGEMENT SYSTEM");
            Console.WriteLine(
                "==============================================");

            while (running)
            {
                Console.WriteLine("\n============== MAIN MENU ==============");
                Console.WriteLine("1.  List all courses");
                Console.WriteLine("2.  Search course");
                Console.WriteLine("3.  Sort courses by fee");
                Console.WriteLine("4.  Register learner");
                Console.WriteLine("5.  List learners");
                Console.WriteLine("6.  Search learner");
                Console.WriteLine("7.  Enroll in a course");
                Console.WriteLine("8.  View enrollments");
                Console.WriteLine("9.  Cancel enrollment");
                Console.WriteLine("10. Calculate fee");
                Console.WriteLine("11. Process payment");
                Console.WriteLine("12. View payment history");
                Console.WriteLine("13. Display reports");
                Console.WriteLine("14. Exit");
                Console.WriteLine("=======================================");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice.Trim())
                {
                    case "1":
                        trainingService.DisplayAllCourses();
                        break;

                    case "2":
                        Console.Write("Enter course code or exact title: ");
                        string searchText = Console.ReadLine() ?? "";

                        trainingService.SearchCourse(searchText);
                        break;

                    case "3":
                        Console.WriteLine("1. Ascending");
                        Console.WriteLine("2. Descending");
                        Console.Write("Choose sorting order: ");

                        string sortChoice = Console.ReadLine() ?? "";

                        if (sortChoice == "1")
                        {
                            trainingService.SortCoursesByFee(true);
                        }
                        else if (sortChoice == "2")
                        {
                            trainingService.SortCoursesByFee(false);
                        }
                        else
                        {
                            Console.WriteLine("Invalid sorting choice.");
                        }
                        break;

                    case "4":
                        Console.Write("Enter learner ID: ");
                        string learnerId = Console.ReadLine() ?? "";

                        Console.Write("Enter learner name: ");
                        string name = Console.ReadLine() ?? "";

                        Console.Write("Enter contact number: ");
                        string contact = Console.ReadLine() ?? "";

                        Console.Write("Enter email: ");
                        string email = Console.ReadLine() ?? "";

                        learnerService.RegisterLearner(
                            learnerId, name, contact, email);
                        break;

                    case "5":
                        learnerService.DisplayAllLearners();
                        break;

                    case "6":
                        Console.Write("Enter learner ID: ");
                        string id = Console.ReadLine() ?? "";

                        try
                        {
                            learnerService.SearchLearner(id);
                        }
                        catch (InvalidLearnerIdException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "7":
                        Console.Write("Enter learner ID: ");
                        string enrollLearnerId =
                            Console.ReadLine() ?? "";

                        Console.Write("Enter course code: ");
                        string courseCode =
                            Console.ReadLine() ?? "";

                        Console.Write("Session type (1=Online, 2=Classroom): ");
                        string sessionChoice =
                            Console.ReadLine() ?? "";

                        if (sessionChoice != "1" &&
                            sessionChoice != "2")
                        {
                            Console.WriteLine("Invalid session type.");
                            break;
                        }

                        bool isOnline = sessionChoice == "1";

                        Console.Write("Is this an early-bird enrollment? (y/n): ");
                        string earlyChoice =
                            Console.ReadLine() ?? "";

                        bool isEarlyBird =
                            earlyChoice.Trim().Equals(
                                "y",
                                StringComparison.OrdinalIgnoreCase);

                        Console.Write("Number of eligible group learners: ");

                        if (!int.TryParse(
                            Console.ReadLine(), out int groupSize) ||
                            groupSize < 1)
                        {
                            Console.WriteLine(
                                "Enter a valid group size of at least 1.");
                            break;
                        }

                        enrollmentService.EnrollLearner(
                            enrollLearnerId,
                            courseCode,
                            isOnline,
                            isEarlyBird,
                            groupSize);
                        break;

                    case "8":
                        enrollmentService.DisplayEnrollments();
                        break;

                    case "9":
                        Console.Write("Enter enrollment ID to cancel: ");

                        if (int.TryParse(
                            Console.ReadLine(), out int cancelId))
                        {
                            enrollmentService.CancelEnrollment(cancelId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid enrollment ID.");
                        }
                        break;

                    case "10":
                        Console.Write("Enter base course fee: ");

                        if (!decimal.TryParse(
                            Console.ReadLine(), out decimal baseFee) ||
                            baseFee < 0)
                        {
                            Console.WriteLine("Enter a valid non-negative fee.");
                            break;
                        }

                        Console.Write("Online session? (y/n): ");
                        string onlineChoice =
                            Console.ReadLine() ?? "";

                        bool feeIsOnline =
                            onlineChoice.Trim().Equals(
                                "y",
                                StringComparison.OrdinalIgnoreCase);

                        Console.Write("Early-bird eligible? (y/n): ");
                        string earlyFeeChoice =
                            Console.ReadLine() ?? "";

                        bool feeIsEarlyBird =
                            earlyFeeChoice.Trim().Equals(
                                "y",
                                StringComparison.OrdinalIgnoreCase);

                        Console.Write("Group size: ");

                        if (!int.TryParse(
                            Console.ReadLine(), out int feeGroupSize) ||
                            feeGroupSize < 1)
                        {
                            Console.WriteLine("Invalid group size.");
                            break;
                        }

                        try
                        {
                            var fee = feeCalculator.CalculateFee(
                                baseFee,
                                feeIsOnline,
                                feeIsEarlyBird,
                                feeGroupSize);

                            feeCalculator.DisplayFeeBreakdown(
                                fee.BaseFee,
                                fee.DiscountAmount,
                                fee.FinalFee,
                                feeIsOnline);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "11":
                        Console.Write("Enter enrollment ID: ");

                        if (!int.TryParse(
                            Console.ReadLine(), out int paymentEnrollmentId))
                        {
                            Console.WriteLine("Invalid enrollment ID.");
                            break;
                        }

                        Console.WriteLine("1. Simulate successful payment");
                        Console.WriteLine("2. Simulate failed payment");
                        Console.Write("Choose payment result: ");

                        string paymentChoice =
                            Console.ReadLine() ?? "";

                        if (paymentChoice == "1")
                        {
                            paymentService.ProcessPayment(
                                paymentEnrollmentId, true);
                        }
                        else if (paymentChoice == "2")
                        {
                            paymentService.ProcessPayment(
                                paymentEnrollmentId, false);
                        }
                        else
                        {
                            Console.WriteLine("Invalid payment choice.");
                        }
                        break;

                    case "12":
                        paymentService.DisplayPaymentHistory();
                        break;

                    case "13":
                        reportService.DisplayAllReports();
                        break;

                    case "14":
                        running = false;
                        Console.WriteLine(
                            "Thank you for using the Training Management System!");
                        break;

                    default:
                        Console.WriteLine(
                            "Invalid choice. Please enter a number from 1 to 14.");
                        break;
                }
            }
        }
    }
}
