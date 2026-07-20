import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { PlanRequest, PlanResponse } from '../Models/Plan'

@Injectable({
  providedIn: 'root'
})
export class PlanService {

  private apiUrl = 'https://localhost:7083/api/PolicyPlans';

  constructor(private http: HttpClient) { }

  getByProduct(productId: number): Observable<PlanResponse[]> {

    return this.http.get<PlanResponse[]>(
      `${this.apiUrl}/product/${productId}`
    );

  }

  add(request: PlanRequest): Observable<PlanResponse> {

    return this.http.post<PlanResponse>(
      this.apiUrl,
      request
    );

  }

  update(id: number, request: PlanRequest): Observable<PlanResponse> {

    return this.http.put<PlanResponse>(
      `${this.apiUrl}/${id}`,
      request
    );

  }

  deactivate(id: number): Observable<any> {

    return this.http.put(
      `${this.apiUrl}/deactivate/${id}`,
      {}
    );

  }

}