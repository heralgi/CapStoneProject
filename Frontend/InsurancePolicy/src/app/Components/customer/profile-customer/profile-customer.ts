import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerResponse } from '../../../Models/Customer-model'; // Adjust path as needed
import { CustomerService } from '../../../services/customer-service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-customer-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './profile-customer.html'
})
export class ProfileCustomer implements OnInit {

  customer = signal<CustomerResponse>(null as unknown as CustomerResponse);
  fb = inject(FormBuilder);
  editForm = this.fb.nonNullable.group({
  fullName: [''],
  email: [''],
  mobileNumber: [''],
  dateOfBirth: [''],
  address: [''],
  city: [''],
  state: [''],
  pinCode: [''],
  nomineeName: [''],
  nomineeRelation: ['']
});

  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    // Simulated API response lookup
    this.loadCustomerProfile();
  }

  showEditModal = signal<boolean>(false);

openEditModal() {
  const customer = this.customer();

  if (!customer) return;

  this.editForm.patchValue({
    fullName: customer.fullName,
    email: customer.email,
    mobileNumber: customer.mobileNumber,
    dateOfBirth:
    customer.dateOfBirth instanceof Date
      ? customer.dateOfBirth.toISOString().split('T')[0]
      : customer.dateOfBirth.split('T')[0],
    address: customer.address,
    city: customer.city,
    state: customer.state,
    pinCode: customer.pinCode,
    nomineeName: customer.nomineeName,
    nomineeRelation: customer.nomineeRelation
  });

  this.showEditModal.set(true);
}

closeEditModal() {
  this.showEditModal.set(false);
}

  loadCustomerProfile(): void {
    this.customerService.getMyProfile().subscribe({
      next: (customer) => {
        this.customer.set(customer);
      },
      error: (error) => {
        console.error('Error fetching customer profile:', error);
      }
    });
  }

  save() {
  if (this.editForm.invalid)
    return;

  const request = this.editForm.getRawValue();

  this.customerService.putCustomerProfile(request as CustomerResponse, this.customer().customerId).subscribe({
    next: (data) => {

      // Update signal
      // this.customer.set(data);
      this.loadCustomerProfile();
      this.showEditModal.set(false);
    }
  });
}

  getInitials(name?: string): string {
    if (!name) return '';
    return name.split(' ').map(n => n[0]).join('').toUpperCase().substring(0, 2);
  }

  onPhotoSelected(event: Event): void {

  const input = event.target as HTMLInputElement;

  if (!input.files?.length) {
    return;
  }

  const file = input.files[0];

  // Call your API to upload to Cloudinary
  this.customerService.uploadProfileImage(file, this.customer().customerId  ).subscribe({
    next: response => {
      this.loadCustomerProfile(); // Refresh profile
    },
    error: error => {
      console.error('Error uploading profile image:', error);
    }
  });
}
}
