import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  name: string;
  email: string;
  password: string;
  registerNumber: string;
  department: string;
  year: number;
  phone: string;
}

export interface LoginResponse {
  token: string;
  userId: number;
  name: string;
  email: string;
  role: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl =
    'https://localhost:7143/api/auth';

  private loggedInSubject =
    new BehaviorSubject<boolean>(
      this.isLoggedIn()
    );

  loggedIn$ =
    this.loggedInSubject.asObservable();

  private roleSubject =
    new BehaviorSubject<string | null>(
      this.getRole()
    );

  role$ =
    this.roleSubject.asObservable();

  constructor(private http: HttpClient) {}

  login(data: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${this.apiUrl}/login`,
      data
    );
  }

  register(data: RegisterRequest): Observable<any> {
    return this.http.post(
      `${this.apiUrl}/register`,
      data
    );
  }

  saveToken(token: string): void {

    localStorage.setItem('token', token);

    this.loggedInSubject.next(true);
    this.roleSubject.next(this.getRole());
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRole(): string | null {

    const token = this.getToken();

    if (!token) {
      return null;
    }

    try {

      const payload =
        JSON.parse(
          atob(token.split('.')[1])
        );

      return payload[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ] || null;

    } catch {

      return null;
    }
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout(): void {

    localStorage.removeItem('token');

    this.loggedInSubject.next(false);
    this.roleSubject.next(null);
  }
}