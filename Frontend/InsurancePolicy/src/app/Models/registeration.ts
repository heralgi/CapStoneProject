export interface RegisterRequest {
  fullName: string;
  email: string;
  password: string;
  mobileNumber: string;
  dateOfBirth: string;   // yyyy-MM-dd or ISO string
  address: string;
  city: string;
  state: string;
  pinCode: string;
  nomineeName: string;
  nomineeRelation: string;
}

export interface RegisterResponse {
  success: boolean;
  message: string;
}
