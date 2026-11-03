import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {

  email = '';
  password = '';
  showPassword = false;

  login() {
    if (!this.email || !this.password) {
      alert('Please enter email and password.');
      return;
    }

    alert('Login UI working! Backend integration will come next.');
  }
}