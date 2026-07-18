export enum ProductType {
  Health = 0,
  Motor = 1,
  Life = 2,
  Travel = 3
}

export interface ProductRequest {
  productName: string;
  productType: number;
  description: string;
  isActive: boolean;
}

export interface ProductResponse {
  productId: number;
  productName: string;
  productType: string;
  description: string;
  isActive: boolean;
  createdDate: Date;
  updatedDate: Date;
}
