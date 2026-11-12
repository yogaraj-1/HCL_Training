import { Component } from '@angular/core';
import { StudentService } from '../../services/student-service';
import { studentRecord } from '../../services/student-service';
import { StudentCard } from '../student-card/student-card';
@Component({
  selector: 'app-student-list',
  imports: [StudentCard],
  templateUrl: './student-list.html',
  styleUrl: './student-list.css',
})
export class StudentList {
  students : studentRecord[]=[];
  constructor(private studentService:StudentService){
  this.students=this.studentService.getStudents();
}
}
