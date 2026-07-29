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
}
