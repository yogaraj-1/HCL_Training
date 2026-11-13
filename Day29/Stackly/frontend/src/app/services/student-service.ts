import { Injectable } from '@angular/core';

export interface studentRecord{
    Name:string;
    Age:number;
    Email:string;
    Course:string;
    RegistrationDate: Date;
    Price: number;
  }
  
@Injectable({
  providedIn: 'root',
})


export class StudentService{
 students:studentRecord[]=[];

 addStudent(student:studentRecord):void{
  this.students.push(student);
 }
 getStudents():studentRecord[]{
  return this.students;
 }

}