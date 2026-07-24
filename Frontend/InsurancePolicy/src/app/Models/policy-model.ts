export interface PolicyResponse {

  policyId: number;

  policyNumber: string;

  customerId: number;

  planName: string;

  coverageAmount: number;

  premiumAmount: number;

  premiumType: string;

  startDate: string;

  endDate: string;

  policyStatus: string;

  aadharNumber: string,

  vehicleNumber: string,

  totalPremiumPaid: number;

}

export interface CustomerPolicyPurchaseRequest {

  planId: number;

  identifier: string;

  startDate: string;

}