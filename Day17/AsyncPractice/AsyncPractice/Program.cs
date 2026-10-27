//Extension method example 1
/*using System;

namespace RetailOrderConsole
{
    public static class StringExtensions
    {
        public static string Clean(this string msg)
        {
            return msg.Trim();
        }

        public static string UpperCase(this string msg)
        {
            return msg.ToUpper();
        }

        public static string AddPrefix(this string msg, string prefix)
        {
            return $"{prefix} " + msg;
        }

        public static string AddSuffix(this string msg, string suffix)
        {
            return $"{msg} - {suffix}";
        }

        public static int Add(this int a, int b)
        {
            return a + b;
        }
    }

    public class LearnExtensionMethods
    {
        public void Run()
        {
            string s1 = "Sakthivel";
            Console.WriteLine(s1);

            string s2 = s1.Clean().UpperCase().AddPrefix("Mr.");
            Console.WriteLine(s2);

            string s3 = s1.Clean()
                           .UpperCase()
                           .AddPrefix("Mr.")
                           .AddSuffix("Tech Lead");
            Console.WriteLine(s3);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LearnExtensionMethods demo = new LearnExtensionMethods();
            demo.Run();

            Console.ReadKey();
        }
    }
}
*/

//Extensions Method 2 created
/*
using RetailOrderConsole;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RetailOrderConsole
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public int Age { get; set; }
    }

    public static class StudentExtensions
    {
        public static void Display(this Student student)
        {
            //Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Student ID   : {student.Id}");
            Console.WriteLine($"Student Name : {student.Name}");
            Console.WriteLine($"Student Dept : {student.Department}");
            Console.WriteLine($"Student Age  : {student.Age}");
            Console.WriteLine("----------------------------------------------");
        }

        public static void Display(this List<Student> students)
        {
            foreach (Student student in students)
            {
                student.Display();
            }

        }

        public static bool IsAdult(this Student student)
        {
            return student.Age >= 18;
        }

        public static List<Student> GetAdults(this List<Student> students)
        {
            List<Student> adultStudents = new List<Student>();
            foreach (Student student in students)
            {
                if (student.IsAdult())
                {
                    adultStudents.Add(student);
                }
            }

            return adultStudents;
        }

        public static List<Student> GetByDepartment(this List<Student> students, string dept)
        {
            List<Student> filteredStudents = students
                .Where(s => s.Department.Equals(dept, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return filteredStudents;
        }

        public static Student GetByName(this List<Student> students, string name)
        {
            Student s=students.Where(s=>s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            return s;
        }

        public static List<Student> GetByMinimumAge(this List<Student> students, int minAge)
        {
            return students.Where(s => s.Age >= minAge).ToList();
        }

    }

    public class LearnExtensionMethodsOnObjects
    {
        public void Run()
        {
            List<Student> students = new List<Student>
                                    {
                                        new Student
                                        {
                                            Id = 101,
                                            Name = "Arun",
                                            Department = "CSE",
                                            Age = 21
                                        },
                                        new Student
                                        {
                                            Id = 102,
                                            Name = "Priya",
                                            Department = "ECE",
                                            Age = 19
                                        },
                                        new Student
                                        {
                                            Id = 103,
                                            Name = "Karthik",
                                            Department = "IT",
                                            Age = 17
                                        },
                                        new Student
                                        {
                                            Id = 104,
                                            Name = "Divya",
                                            Department = "CSE",
                                            Age = 22
                                        },
                                        new Student
                                        {
                                            Id = 105,
                                            Name = "Rahul",
                                            Department = "EEE",
                                            Age = 20
                                        },
                                        new Student
                                        {
                                            Id = 106,
                                            Name = "Sneha",
                                            Department = "IT",
                                            Age = 18
                                        },
                                        new Student
                                        {
                                            Id = 107,
                                            Name = "Vikram",
                                            Department = "ECE",
                                            Age = 23
                                        },
                                        new Student
                                        {
                                            Id = 108,
                                            Name = "Meena",
                                            Department = "CSE",
                                            Age = 17
                                        },
                                        new Student
                                        {
                                            Id = 109,
                                            Name = "Sanjay",
                                            Department = "IT",
                                            Age = 24
                                        },
                                        new Student
                                        {
                                            Id = 110,
                                            Name = "Kavya",
                                            Department = "EEE",
                                            Age = 17
                                        }
                                    };
            // Extension Methods

            // students[0].Display();

            // studentsList.Display();

            // Console.WriteLine(students[0].IsAdult());

            // students.GetAdults().Display();

            //students.GetByDepartment("EEe").Display();

            // students.GetAdults().GetByDepartment("CSE").Display();

            // students.GetByDepartment("CSE").GetByMinimumAge(21).Display();
            students.GetByName("kavya").Display();


        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        LearnExtensionMethodsOnObjects demo =
            new LearnExtensionMethodsOnObjects();

        demo.Run();

        Console.ReadKey();
    }
}
*/

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

class Program
{
    static async Task Main(string[] args)
    {
        LearnAsyncProgramming demo = new LearnAsyncProgramming();

        await demo.Run();

        Console.WriteLine("Program finished.");
        Console.ReadKey();
    }
}