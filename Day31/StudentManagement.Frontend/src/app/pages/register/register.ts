import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { AuthService } from '../../core/services/auth';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class RegisterComponent {

  registerForm = new FormGroup({

    name: new FormControl('', [
      Validators.required
    ]),

    email: new FormControl('', [
      Validators.required,
      Validators.email
    ]),

    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6)
    ]),

    registerNumber: new FormControl('', [
      Validators.required
    ]),

    department: new FormControl('', [
      Validators.required
    ]),

    year: new FormControl<number | null>(null, [
      Validators.required,
      Validators.min(1),
      Validators.max(4)
    ]),

    phone: new FormControl('', [
      Validators.required
    ])

  });

  message = '';
  errorMessage = '';

  constructor(
    private authService: AuthService
  ) {}

  onSubmit(): void {

    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    this.message = '';
    this.errorMessage = '';

    const data = {
      name: this.registerForm.value.name!,
      email: this.registerForm.value.email!,
      password: this.registerForm.value.password!,
      registerNumber: this.registerForm.value.registerNumber!,
      department: this.registerForm.value.department!,
      year: this.registerForm.value.year!,
      phone: this.registerForm.value.phone!
    };

    this.authService.register(data).subscribe({

      next: (response) => {

        console.log(
          'Registration successful:',
          response
        );

        this.message =
          'Registration successful! You can now login.';

        this.registerForm.reset();
      },

      error: (error) => {

        console.error(
          'Registration failed:',
          error
        );

        this.errorMessage =
          error.error?.message ||
          'Registration failed.';
      }

    });
  }
}