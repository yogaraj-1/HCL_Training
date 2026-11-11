/*create a student management data structure where an inteface called student and the properties are id,name ,age ,deprtment, and optional email property ,a function to display student details  which contains an array of five studets .
a union type for student id can be number or string and an intersection type combing students and contact details */



interface Student {
    id: number |string ,
    name:string,
    age :number,
    department:string,
    email?:string
}


interface  ContactDetails{
  phone: string;
  city: string;
}

type StudentWithContact = Student & ContactDetails;

const students: StudentWithContact[] = [
  {
    id: 101,
    name: "Aarav Kumar",
    age: 20,
    department: "Computer Science",
    email: "aarav.k@example.edu",
    phone: "9876543210",
    city: "Chennai",
  },
  {
    id: "STU-102",
    name: "Priya Dharshini",
    age: 21,
    department: "Electronics",
    phone: "9876543211",
    city: "Madurai",
  },
  {
    id: 103,
    name: "Vikram Selvam",
    age: 19,
    department: "Mechanical",
    email: "vikram.s@example.edu",
    phone: "9876543212",
    city: "Coimbatore",
  },
  {
    id: "STU-104",
    name: "Divya Bharathi",
    age: 22,
    department: "Civil",
    phone: "9876543213",
    city: "Trichy",
  },
  {
    id: 105,
    name: "Karthik Subramanian",
    age: 20,
    department: "Information Technology",
    email: "karthik.sub@example.edu",
    phone: "9876543214",
    city: "Salem",
  },
];

function display(StudentList : StudentWithContact[]):void {
    console.log("=== Student Management Directory ===\n");

    StudentList.forEach((student)=>
        {
            const emailDetail=student.email ?? "Not Provided";
            console.log(`ID         : ${student.id}`);
    console.log(`Name       : ${student.name}`);
    console.log(`Age        : ${student.age}`);
    console.log(`Department : ${student.department}`);
    console.log(`Email      : ${emailDetail}`);
    console.log(`Phone      : ${student.phone}`);
    console.log(`City       : ${student.city}`);
    console.log("------------------------------------");

    });
    
}
display(students);