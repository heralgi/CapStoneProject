import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest } from '../Models/LoginRequest';
import { AuthResponse } from '../Models/AuthResponse';
import { RegisterRequest, RegisterResponse } from '../Models/registeration';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7083/api/Auth';

  constructor(private http: HttpClient) {}

  login(data: LoginRequest): Observable<AuthResponse> {

    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, data);

  }

  register(request: RegisterRequest) {
  return this.http.post<RegisterResponse>(
    `${this.apiUrl}/register`,
    request
  );

  }
}