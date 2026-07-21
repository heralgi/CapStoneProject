import { HttpClient } from '@angular/common/http';
import { Injectable, Service } from '@angular/core';
import { CreateUserRequest, UpdateUserRequest, UserResponse } from '../Models/user-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
    private apiUrl = 'https://localhost:7083/api/Users';

  constructor(private http: HttpClient) { }

  getAll(): Observable<UserResponse[]> {
    return this.http.get<UserResponse[]>(`${this.apiUrl}/getAll`);
  }

  postUser(user: CreateUserRequest): Observable<UserResponse>{
    return this.http.post<UserResponse>(this.apiUrl, user);
  }
  
  putUser(Id: number, user: UpdateUserRequest): Observable<UserResponse>{
    return this.http.put<UserResponse>(`${this.apiUrl}/${Id}`, user);
  }
}
