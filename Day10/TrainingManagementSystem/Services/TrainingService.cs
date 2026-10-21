
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class TrainingService
    {
        private readonly Course[] courses;

        public TrainingService(Course[] courses)
        {
            this.courses = courses;
        }

        public void DisplayAllCourses()
        {
            Console.WriteLine("\n================ COURSE CATALOGUE ================");

            Console.WriteLine(
                "{0,-12} {1,-25} {2,12} {3,12} {4,12}",
                "Course Code",
                "Title",
                "Fee",
                "Duration",
                "Available");

            Console.WriteLine(new string('-', 78));

            foreach (Course course in courses)
            {
                int availableSeats =
                    course.TotalSeats - course.EnrolledCount;

                Console.WriteLine(
                    "{0,-12} {1,-25} {2,12:C0} {3,9} days {4,9}",
                    course.CourseCode,
                    course.Title,
                    course.Fee,
                    course.DurationDays,
                    availableSeats);
            }
        }

        public Course? SearchCourse(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Console.WriteLine("Search input cannot be empty.");
                return null;
            }

            foreach (Course course in courses)
            {
                if (string.Equals(
                        course.CourseCode,
                        searchText.Trim(),
                        StringComparison.OrdinalIgnoreCase)
                    ||
                    string.Equals(
                        course.Title,
                        searchText.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nCourse found:");
                    DisplaySingleCourse(course);
                    return course;
                }
            }

            Console.WriteLine("Course not found.");
            return null;
        }

        public void SortCoursesByFee(bool ascending)
        {
            // Create a copy so the original catalogue remains unchanged.
            Course[] sortedCourses = (Course[])courses.Clone();

            // Bubble sort without LINQ.
            for (int i = 0; i < sortedCourses.Length - 1; i++)
            {
                for (int j = 0; j < sortedCourses.Length - i - 1; j++)
                {
                    bool shouldSwap = ascending
                        ? sortedCourses[j].Fee > sortedCourses[j + 1].Fee
                        : sortedCourses[j].Fee < sortedCourses[j + 1].Fee;

                    if (shouldSwap)
                    {
                        Course temp = sortedCourses[j];
                        sortedCourses[j] = sortedCourses[j + 1];
                        sortedCourses[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine(
                ascending
                    ? "\nCourses sorted by fee (ascending):"
                    : "\nCourses sorted by fee (descending):");

            foreach (Course course in sortedCourses)
            {
                Console.WriteLine(
                    $"{course.CourseCode,-10} {course.Title,-25} {course.Fee,10:C0}");
            }
        }

        private void DisplaySingleCourse(Course course)
        {
            Console.WriteLine($"Course Code    : {course.CourseCode}");
            Console.WriteLine($"Title          : {course.Title}");
            Console.WriteLine($"Fee            : {course.Fee:C}");
            Console.WriteLine($"Duration       : {course.DurationDays} days");
            Console.WriteLine($"Total Seats    : {course.TotalSeats}");
            Console.WriteLine(
                $"Enrolled       : {course.EnrolledCount}");
            Console.WriteLine(
                $"Available Seats: {course.TotalSeats - course.EnrolledCount}");
        }
    }
}
