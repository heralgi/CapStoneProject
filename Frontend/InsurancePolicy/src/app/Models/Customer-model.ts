export interface CustomerResponse {
  customerId: number;
  fullName: string;
  email: string;
  mobileNumber: string;
  dateOfBirth: Date | string; // Matches both a structural JavaScript Date object or a parsed JSON ISO string
  address: string;
  city: string;
  state: string;
  pinCode: string;
  nomineeName: string;
  nomineeRelation: string;
}
