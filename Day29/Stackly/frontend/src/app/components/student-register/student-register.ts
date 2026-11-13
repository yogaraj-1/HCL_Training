import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import {studentRecord} from '../../services/student-service';
import {StudentService} from '../../services/student-service';
@Component({
  selector: 'app-student-register',
  imports: [FormsModule],
  templateUrl: './student-register.html',
  styleUrl: './student-register.css',
})
export class StudentRegister {
  studentName: string = '';
  studentAge: number = 0;
  studentEmail: string = '';
  studentCourse: string = '';

coursePrices: { [key: string]: number } = {
  Angular: 15000,
  React: 14000,
  Java: 18000,
  Python: 16000
};

  constructor(
    private router: Router,
    private studentService: StudentService
  ) {}
addStudent(){
  const student:studentRecord={
      Name: this.studentName,
      Age:this.studentAge,
      Email:this.studentEmail,
      Course:this.studentCourse,
      RegistrationDate: new Date(),
      Price: this.coursePrices[this.studentCourse],
  }
  this.studentService.addStudent(student);
  this.router.navigate(['/student-list']);
}

}
