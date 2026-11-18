import { Component } from '@angular/core';

import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { Router, RouterLink } from '@angular/router';

import { AuthService } from '../../core/services/auth';
@Component({
  selector: 'app-login',
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class LoginComponent {

  loginForm = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ]),

    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6)
    ])
  });

  errorMessage = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit(): void {

    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.errorMessage = '';

    const data = {
      email: this.loginForm.value.email!,
      password: this.loginForm.value.password!
    };

    this.authService.login(data).subscribe({

      next: (response) => {

        console.log('Login successful:', response);

        this.authService.saveToken(response.token);

        if (response.role === 'Admin') {
          this.router.navigate(['/admin/dashboard']);
        }
        else {
          this.router.navigate(['/student/dashboard']);
        }
      },

      error: (error) => {

        console.error('Login failed:', error);

        this.errorMessage =
          error.error?.message ||
          'Invalid email or password.';
      }

    });
  }
}