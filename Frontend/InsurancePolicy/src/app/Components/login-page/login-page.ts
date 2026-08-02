import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ToastService } from 'ngx-toastr-notifier';

import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login-page.html',
  styleUrls: ['./login-page.css']
})
export class LoginComponent implements OnInit {

  errorMessage = '';

  private readonly authService = inject(AuthService);
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly toastService = inject(ToastService);

  loginForm = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  constructor() { }
  ngOnInit(): void {

    // If already logged in, go directly to dashboard
    if (this.authService.isLoggedIn()) {

      this.router.navigateByUrl(
        this.authService.getDashboardRoute()
      );

      return;

    }

    // Demo credentials
    this.loginForm.patchValue({
      email: 'admin@example.com',
      password: '1234'
    });

  }

  login(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }
    this.errorMessage = '';
    this.authService.login(this.loginForm.getRawValue()).subscribe({
      next: () => {
        this.toastService.success('You have been logged in successfully!', 'Success');
        this.toastService.success('Data synchronized!', 'Sync Complete', {
    duration: 5000,                  // Stays on screen for 5 seconds (in ms)
    showClose: false,                // Hides the close 'X' button
    horizontalPosition: 'center',    // Options: 'left' | 'center' | 'right' | 'start' | 'end'
    verticalPosition: 'bottom'       // Options: 'top' | 'bottom'
  });
        // Token is already stored in AuthService
        this.router.navigateByUrl(
          this.authService.getDashboardRoute()
        );
      },
      error: (err) => {

        this.errorMessage =
          err.error?.message ??
          'Invalid Email or Password';
      }
    });
  }
  register(): void {
    this.router.navigate(['/register']);
  }
}