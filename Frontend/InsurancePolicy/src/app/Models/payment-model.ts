export enum PaymentMode {
  UPI = 0,
  Card = 1,
  NetBanking = 2,
  Cash = 3
}

export enum PaymentStatus {
  Success = 0,
  Failed = 1,
  Pending = 2
}

export interface PaymentRequest {
  policyId: number;
  amount: number;
  paymentMode: PaymentMode;
  transactionReference: string;
  paymentDate: string; // ISO date string (YYYY-MM-DDTHH:mm:ssZ)
  paymentStatus: PaymentStatus;
}

export interface PolicyPaymentResponse {
  paymentId: number;
  policyNumber: string;
  amount: number;
  paymentDate: string;
  paymentMode: string;       // .NET converts this to a friendly string label
  transactionReference: string;
  paymentStatus: string;     // .NET converts this to a friendly string label
}
