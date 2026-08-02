import { HttpClient } from '@angular/common/http';
import { Injectable, Service } from '@angular/core';
import { CustomerResponse } from '../Models/Customer-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

    private apiUrl = 'https://localhost:7083/api/Customers';

  constructor(private http: HttpClient) { }

  getAll(): Observable<CustomerResponse[]> {
    return this.http.get<CustomerResponse[]>(`${this.apiUrl}/getAll`);
  }

  getMyProfile(): Observable<CustomerResponse> {
    return this.http.get<CustomerResponse>(`${this.apiUrl}/getMyProfile`);
  }

  putCustomerProfile(customer: CustomerResponse, id: number): Observable<CustomerResponse> {
    return this.http.put<CustomerResponse>(`${this.apiUrl}/${id}`, customer);
  }

  uploadProfileImage(file: File, customerId: number): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post(`${this.apiUrl}/${customerId}/profile-image`, formData);
  }
}
