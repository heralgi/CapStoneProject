export interface PlanRequest {

  productId: number;

  planName: string;

  coverageAmount: number;

  premiumAmount: number;

  premiumType: number;

  durationYears: number;

  termsAndConditions: string;

  isActive: boolean;

}

export interface PlanResponse {

  planId: number;

  productId: number;

  planName: string;
  coverageAmount: number;

  premiumAmount: number;

  premiumType: number;

  durationYears: number;

  termsAndConditions: string;  

  isActive: boolean;

}