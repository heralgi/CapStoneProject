import { Component, inject } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth';
import { Router } from '@angular/router';
import { RegisterRequest } from '../../Models/registeration';

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  errorMessage = '';
  private readonly fb = inject(FormBuilder);
  private readonly authService = inject(AuthService);

  registerForm = this.fb.nonNullable.group({
    fullName: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    mobileNumber: ['', [Validators.required]],
    dateOfBirth: [''],
    address: [''],
    city: [''],
    state: [''],
    pinCode: [''],
    nomineeName: [''],
    nomineeRelation: ['']
  });

register(): void {

  if (this.registerForm.invalid) {
    return;
  }

  const request: RegisterRequest = this.registerForm.getRawValue();

  this.authService.register(request).subscribe({
    next: (response) => {
      console.log(response);
    },
    error: (err) => {
      console.log(err);
    }
  });
}
}
