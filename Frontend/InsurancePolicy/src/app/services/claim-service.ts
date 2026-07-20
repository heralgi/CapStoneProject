import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ClaimRequest, ClaimReviewRequest, ClaimResponse } from '../Models/claim-model';

@Injectable({
  providedIn: 'root'
})
export class ClaimService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7083/api/Claims';

  // GET api/claims
  getClaims(): Observable<ClaimResponse> {
    return this.http.get<any>(this.apiUrl);
  }

  getAllClaims(): Observable<ClaimResponse[]> {
    return this.http.get<ClaimResponse[]>(`${this.apiUrl}/getAll`);
  }

  // GET api/claims/policy/5
  getClaimsByPolicy(policyId: number): Observable<ClaimResponse[]> {
    return this.http.get<ClaimResponse[]>(
      `${this.apiUrl}/policy/${policyId}`
    );
  }

  // POST api/claims/raise
  raiseClaim(request: ClaimRequest): Observable<ClaimResponse> {
    return this.http.post<ClaimResponse>(
      `${this.apiUrl}/raise`,
      request
    );
  }

  // PUT api/claims/review/5
  reviewClaim(
    claimId: number,
    request: ClaimReviewRequest
  ): Observable<ClaimResponse> {
    return this.http.put<ClaimResponse>(
      `${this.apiUrl}/review/${claimId}`,
      request
    );
  }

  // PUT api/claims/approve/5
  approveClaim(claimId: number): Observable<ClaimResponse> {
    return this.http.put<ClaimResponse>(
      `${this.apiUrl}/approve/${claimId}`,
      {}
    );
  }

  // PUT api/claims/reject/5
  rejectClaim(claimId: number): Observable<ClaimResponse> {
    return this.http.put<ClaimResponse>(
      `${this.apiUrl}/reject/${claimId}`,
      {}
    );
  }

}