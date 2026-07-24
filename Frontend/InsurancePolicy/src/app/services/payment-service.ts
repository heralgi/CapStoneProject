import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaymentRequest, PolicyPaymentResponse } from '../Models/payment-model';

@Injectable({
  providedIn: 'root'
})
export class PaymentsService {
  private http = inject(HttpClient);
  // Replace with your true backend API gateway root URL
  private apiUrl = 'https://localhost:7083/api/PremiumPayments';

  /** GET: api/premiumpayments */
  getPaymentHistory(): Observable<PolicyPaymentResponse[]> {
    return this.http.get<PolicyPaymentResponse[]>(this.apiUrl);
  }

  /** GET: api/premiumpayments/policy/5 */
  getPaymentsByPolicy(policyId: number): Observable<PolicyPaymentResponse[]> {
    return this.http.get<PolicyPaymentResponse[]>(`${this.apiUrl}/policy/${policyId}`);
  }

  /** POST: api/premiumpayments */
  recordPayment(payment: PaymentRequest): Observable<PolicyPaymentResponse> {
    return this.http.post<PolicyPaymentResponse>(this.apiUrl, payment);
  }
}
