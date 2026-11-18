import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { HttpClient } from '@angular/common/http';

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

@Component({
  selector: 'app-student-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './student-dashboard.html',
  styleUrl: './student-dashboard.css'
})
export class StudentDashboardComponent implements OnInit {

  student: Student | null = null;

  loading = true;

  errorMessage = '';

  private apiUrl =
    'https://localhost:7143/api/students';

  constructor(
    private http: HttpClient,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

    console.log('DASHBOARD INIT');

    this.loadStudentProfile();
  }

  loadStudentProfile(): void {

    this.http.get<Student>(
      `${this.apiUrl}/me`
    ).subscribe({

      next: (response) => {

        console.log('API RESPONSE:', response);

        this.student = response;

        this.loading = false;

        console.log('Loading:', this.loading);

        this.cdr.detectChanges();
      },

      error: (error) => {

        console.error('API ERROR:', error);

        this.loading = false;

        this.errorMessage =
          'Unable to load your profile.';

        this.cdr.detectChanges();
      }

    });
  }
}