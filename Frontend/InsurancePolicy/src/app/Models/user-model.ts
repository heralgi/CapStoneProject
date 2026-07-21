export interface UserResponse{
    userId: number;
  fullName: string;
  email: string;
  mobileNumber: string;
  role: string;
  isActive: boolean;
  createdDate: string;
}

export interface CreateUserRequest {
  fullName: string;
  email: string;
  password: string;
  mobileNumber: string;
  role: UserRole;
}

export enum UserRole {
  Admin = 0,
  InternalStaff = 1,
  Customer = 2
}

export interface UpdateUserRequest {
  fullName: string;
  mobileNumber: string;
  role: UserRole;
}