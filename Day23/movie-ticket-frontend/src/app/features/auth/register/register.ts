import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class Register {

  fullName = '';
  email = '';
  password = '';
  confirmPassword = '';

  register() {

    if (
      !this.fullName ||
      !this.email ||
      !this.password ||
      !this.confirmPassword
    ) {
      alert('Please fill all fields.');
      return;
    }

    if (this.password !== this.confirmPassword) {
      alert('Passwords do not match.');
      return;
    }

    alert('Registration UI working! Backend integration will come next.');
  }
}