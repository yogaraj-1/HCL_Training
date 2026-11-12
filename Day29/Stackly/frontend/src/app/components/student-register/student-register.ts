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
  }
  this.studentService.addStudent(student);
  this.router.navigate(['/student-list']);
}

}
