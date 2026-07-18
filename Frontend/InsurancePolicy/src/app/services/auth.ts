import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

import { LoginRequest } from '../Models/LoginRequest';
import { AuthResponse } from '../Models/AuthResponse';
import { RegisterRequest, RegisterResponse } from '../Models/registeration';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7083/api/Auth';

  constructor(private http: HttpClient) { }

  // Authentication state
  isAuthenticated = signal<boolean>(this.hasValidToken());

  // Login
  login(data: LoginRequest): Observable<AuthResponse> {

    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, data).pipe(

      tap(response => {

        this.setToken(response.token);

      })

    );

  }

  // Register
  register(request: RegisterRequest): Observable<RegisterResponse> {

    return this.http.post<RegisterResponse>(
      `${this.apiUrl}/register`,
      request
    );

  }

  // Returns true if user is logged in and token is valid
  isLoggedIn(): boolean {

    const isValid = this.hasValidToken();

    this.isAuthenticated.set(isValid);

    return isValid;

  }

  // Returns JWT token
  getToken(): string | null {

    return localStorage.getItem('token');

  }

  // Saves JWT token
  setToken(token: string): void {

    localStorage.setItem('token', token);

    this.isAuthenticated.set(true);

  }

  // Logout user
  logout(): void {

    localStorage.removeItem('token');

    this.isAuthenticated.set(false);

  }

  // Validate JWT token
  private hasValidToken(): boolean {

    const token = localStorage.getItem('token');

    if (!token) {
      return false;
    }

    try {

      const payload = JSON.parse(atob(token.split('.')[1]));

      if (!payload.exp) {

        localStorage.removeItem('token');

        return false;

      }

      const expiry = payload.exp * 1000;

      const isValid = Date.now() < expiry;

      if (!isValid) {

        localStorage.removeItem('token');

      }

      return isValid;

    }
    catch {

      localStorage.removeItem('token');

      return false;

    }

  }

  getRole(): string |null {

  const token = this.getToken();

  if (!token)
    return null;

  try {

    const payload = JSON.parse(atob(token.split('.')[1]));

    return payload.role;

  } catch {

    return null;

  }

}

getDashboardRoute(): string {

  const role = this.getRole();

  switch (role) {

    case 'Admin':
      return '/admin/dashboard';

    case 'InternalStaff':
      return '/staff/dashboard';

    case 'Customer':
      return '/customer/dashboard';

    default:
      return '/login';

  }

}

}