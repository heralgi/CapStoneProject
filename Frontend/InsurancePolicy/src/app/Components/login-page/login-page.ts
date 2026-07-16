import { Component, inject } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login-page.html',
  styleUrls: ['./login-page.css']
})
export class LoginComponent {

  errorMessage = '';
  private readonly authService = inject(AuthService);
  private readonly fb = inject(FormBuilder);
  private router = inject(Router)

  constructor(
    // private fb: FormBuilder,
    // private authService: AuthService,
    // private router: Router
  ) {}

  loginForm = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  ngOnInit() {
    this.loginForm.patchValue({
      email: 'admin@example.com',
      password: '1234'
    });
  }

  login() {

    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.authService.login(this.loginForm.getRawValue()).subscribe({

      next: (response) => {

        localStorage.setItem('token', response.token);

        this.router.navigate(['/dashboard']);
      },

      error: (err) => {

        this.errorMessage = err.error.message ?? 'Invalid Email or Password';

      }

    });

  }

  register() {
    this.router.navigate(['/register']);
  }

}