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

interface NewStudent {
  name: string;
  email: string;
  password: string;
  registerNumber: string;
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

  filteredStudents: Student[] = [];

  searchTerm = '';

  selectedStudent: Student | null = null;

  showAddForm = false;

  adding = false;

  loading = true;

  saving = false;

  errorMessage = '';

  successMessage = '';

  newStudent: NewStudent = {
    name: '',
    email: '',
    password: '',
    registerNumber: '',
    department: '',
    year: 1,
    phone: ''
  };

  private apiUrl =
    'https://localhost:7143/api/students';

  private authApiUrl =
    'https://localhost:7143/api/auth';

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

        this.filteredStudents =
          response;

        this.loading = false;

        this.cdr.detectChanges();

      },

      error: (error) => {

        console.error(
          'STUDENTS ERROR:',
          error
        );

        this.loading = false;

        this.errorMessage =
          'Unable to load students.';

        this.cdr.detectChanges();

      }

    });

  }

  searchStudents(): void {

    const search =
      this.searchTerm
        .trim()
        .toLowerCase();

    if (!search) {

      this.filteredStudents =
        this.students;

      return;

    }

    this.filteredStudents =
      this.students.filter(student =>

        student.name
          .toLowerCase()
          .includes(search)

        ||

        student.email
          .toLowerCase()
          .includes(search)

        ||

        student.registerNumber
          .toLowerCase()
          .includes(search)

        ||

        student.department
          .toLowerCase()
          .includes(search)

      );

  }

  openAddStudentForm(): void {

    this.showAddForm = true;

    this.selectedStudent = null;

    this.errorMessage = '';

    this.successMessage = '';

    this.resetNewStudent();

  }

  cancelAddStudent(): void {

    this.showAddForm = false;

    this.errorMessage = '';

    this.resetNewStudent();

  }

  resetNewStudent(): void {

    this.newStudent = {

      name: '',

      email: '',

      password: '',

      registerNumber: '',

      department: '',

      year: 1,

      phone: ''

    };

  }

  addStudent(): void {

    this.errorMessage = '';

    this.successMessage = '';

    if (
      !this.newStudent.name ||
      !this.newStudent.email ||
      !this.newStudent.password ||
      !this.newStudent.registerNumber ||
      !this.newStudent.department ||
      !this.newStudent.phone
    ) {

      this.errorMessage =
        'Please fill all fields.';

      return;

    }

    this.adding = true;

    this.http.post(
      `${this.authApiUrl}/register`,
      this.newStudent
    ).subscribe({

      next: (response) => {

        console.log(
          'STUDENT CREATED:',
          response
        );

        this.showAddForm = false;

        this.adding = false;

        this.successMessage =
          'Student added successfully.';

        this.resetNewStudent();

        this.loadStudents();

      },

      error: (error) => {

        console.error(
          'ADD STUDENT ERROR:',
          error
        );

        this.adding = false;

        this.errorMessage =
          error.error?.message ||
          'Unable to add student.';

        this.cdr.detectChanges();

      }

    });

  }

  editStudent(student: Student): void {

    this.selectedStudent = {
      ...student
    };

    this.showAddForm = false;

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

      name:
        this.selectedStudent.name,

      department:
        this.selectedStudent.department,

      year:
        this.selectedStudent.year,

      phone:
        this.selectedStudent.phone

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

        this.searchStudents();

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

    const confirmed = confirm(
      'Are you sure you want to delete this student?'
    );

    if (!confirmed) {
      return;
    }

    this.errorMessage = '';

    this.successMessage = '';

    this.http.delete(
      `${this.apiUrl}/${id}`
    ).subscribe({

      next: () => {

        this.students =
          this.students.filter(
            student =>
              student.id !== id
          );

        this.searchStudents();

        this.successMessage =
          'Student deleted successfully.';

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