import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

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


  onSubmit() {

    if (this.registerForm.invalid) {

      this.registerForm.markAllAsTouched();

      return;
    }

    console.log(
      'Registration Data:',
      this.registerForm.value
    );
  }

}