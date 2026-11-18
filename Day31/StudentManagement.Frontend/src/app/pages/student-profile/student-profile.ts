import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import {
  HttpClient
} from '@angular/common/http';

import {
  FormsModule
} from '@angular/forms';

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
  selector: 'app-student-profile',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './student-profile.html',
  styleUrl: './student-profile.css'
})
export class StudentProfileComponent implements OnInit {

  student: Student | null = null;

  loading = true;

  editing = false;

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

    this.loadProfile();

  }

  loadProfile(): void {

    this.loading = true;

    this.http.get<Student>(
      `${this.apiUrl}/me`
    ).subscribe({

      next: (response) => {

        console.log('PROFILE RESPONSE:', response);

        this.student = response;

        this.loading = false;

        this.cdr.detectChanges();

      },

      error: (error) => {

        console.error('PROFILE ERROR:', error);

        this.loading = false;

        this.errorMessage =
          'Unable to load your profile.';

        this.cdr.detectChanges();

      }

    });

  }

  startEditing(): void {

    this.editing = true;

    this.successMessage = '';

    this.errorMessage = '';

  }

  cancelEditing(): void {

    this.editing = false;

    this.successMessage = '';

    this.errorMessage = '';

  }

  updateProfile(): void {

    if (!this.student) {
      return;
    }

    this.saving = true;

    this.errorMessage = '';

    this.successMessage = '';

    const data: UpdateStudent = {

      name: this.student.name,

      department: this.student.department,

      year: this.student.year,

      phone: this.student.phone

    };

    this.http.put<Student>(
      `${this.apiUrl}/${this.student.id}`,
      data
    ).subscribe({

      next: (response) => {

        console.log(
          'PROFILE UPDATED:',
          response
        );

        this.student = response;

        this.saving = false;

        this.editing = false;

        this.successMessage =
          'Profile updated successfully.';

        this.cdr.detectChanges();

      },

      error: (error) => {

        console.error(
          'UPDATE ERROR:',
          error
        );

        this.saving = false;

        this.errorMessage =
          'Unable to update profile.';

        this.cdr.detectChanges();

      }

    });

  }

}