import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import {
  CustomerPolicyPurchaseRequest,
  PolicyResponse,
} from '../Models/policy-model';

@Injectable({
  providedIn: 'root'
})
export class PolicyService {

  private apiUrl = 'https://localhost:7083/api/Policies';

  constructor(private http: HttpClient) { }

  /**
   * GET: api/policies?pageNumber=1&pageSize=10
   */
  getPolicies(): Observable<PolicyResponse[]> {
    return this.http.get<PolicyResponse[]>(`${this.apiUrl}/getAll`);
  }

  getPoliciesByUserId(): Observable<PolicyResponse[]> {
    return this.http.get<PolicyResponse[]>(`${this.apiUrl}/by-user`);
  }

  /**
   * GET: api/policies/POL12345
   */
  getByPolicyNumber(policyNumber: string): Observable<PolicyResponse> {

    return this.http.get<PolicyResponse>(
      `${this.apiUrl}/${policyNumber}`
    );

  }

  /**
   * GET: api/policies/customer/5
   */
  getByCustomer(customerId: number): Observable<PolicyResponse[]> {

    return this.http.get<PolicyResponse[]>(
      `${this.apiUrl}/customer/${customerId}`
    );

  }

  /**
   * POST: api/policies/purchase
   */
  purchase(request: CustomerPolicyPurchaseRequest): Observable<PolicyResponse> {

    return this.http.post<PolicyResponse>(
      `${this.apiUrl}/purchase`,
      request
    );

  }

  /**
   * PUT: api/policies/issue/5
   */
  issue(policyId: number): Observable<PolicyResponse> {

    return this.http.put<PolicyResponse>(
      `${this.apiUrl}/issue/${policyId}`,
      {}
    );

  }

}