export interface ClaimResponse {
  claimId: number;
  claimNumber: string;
  policyNumber: string;
  policyId: number;
  claimDocumentId: number;
  claimAmount: number;
  claimReason: string;
  incidentDate: string;
  claimStatus: string;
  internalStaffRemarks?: string;
  adminRemarks?: string;
  createdDate: string;
  updatedDate: string;
}

export interface ClaimRequest {
  policyId: number;
  claimAmount: number;
  claimReason: string;
  incidentDate: string;
}

export interface ClaimReviewRequest {
  recommendedStatus: number;
  remarks: string;
}