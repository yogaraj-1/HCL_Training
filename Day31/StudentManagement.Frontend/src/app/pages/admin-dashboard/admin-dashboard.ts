import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { FormsModule } from '@angular/forms';

interface Student {
  id: number;
  userId: number;
  name: string;
  email: string;
  registerNumber: string;
  department: string;
  year: number;
  phone: string;
  role: string;
}

interface UpdateStudent {
  name: string;
  department: string;
  year: number;
  phone: string;
}

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css'
})
export class AdminDashboardComponent implements OnInit {

  students: Student[] = [];

  selectedStudent: Student | null = null;

  loading = true;

  saving = false;

  errorMessage = '';

  successMessage = '';

  private apiUrl =
    'https://localhost:7143/api/students';

  constructor(
    private http: HttpClient,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

    this.loadStudents();

  }

  loadStudents(): void {

    this.loading = true;

    this.http.get<Student[]>(
      this.apiUrl
    ).subscribe({

      next: (response) => {

        this.students = response;

        this.loading = false;

        this.cdr.detectChanges();

      },

      error: (error) => {

        console.error('STUDENTS ERROR:', error);

        this.loading = false;

        this.errorMessage =
          'Unable to load students.';

        this.cdr.detectChanges();

      }

    });

  }

  editStudent(student: Student): void {

    this.selectedStudent = {
      ...student
    };

    this.errorMessage = '';

    this.successMessage = '';

  }

  cancelEdit(): void {

    this.selectedStudent = null;

    this.errorMessage = '';

  }

  updateStudent(): void {

    if (!this.selectedStudent) {
      return;
    }

    this.saving = true;

    this.errorMessage = '';

    this.successMessage = '';

    const data: UpdateStudent = {

      name: this.selectedStudent.name,

      department:
        this.selectedStudent.department,

      year: this.selectedStudent.year,

      phone: this.selectedStudent.phone

    };

    this.http.put<Student>(
      `${this.apiUrl}/${this.selectedStudent.id}`,
      data
    ).subscribe({

      next: (response) => {

        console.log(
          'STUDENT UPDATED:',
          response
        );

        const index =
          this.students.findIndex(
            student =>
              student.id === response.id
          );

        if (index !== -1) {

          this.students[index] =
            response;

        }

        this.selectedStudent = null;

        this.saving = false;

        this.successMessage =
          'Student updated successfully.';

        this.cdr.detectChanges();

      },

      error: (error) => {

        console.error(
          'UPDATE ERROR:',
          error
        );

        this.saving = false;

        this.errorMessage =
          'Unable to update student.';

        this.cdr.detectChanges();

      }

    });

  }

  deleteStudent(id: number): void {

    const confirmed =
      confirm(
        'Are you sure you want to delete this student?'
      );

    if (!confirmed) {
      return;
    }

    this.http.delete(
      `${this.apiUrl}/${id}`
    ).subscribe({

      next: () => {

        this.students =
          this.students.filter(
            student => student.id !== id
          );

        this.cdr.detectChanges();

      },

      error: (error) => {

        console.error(
          'DELETE ERROR:',
          error
        );

        this.errorMessage =
          'Unable to delete student.';

        this.cdr.detectChanges();

      }

    });

  }

}