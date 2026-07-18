import { Component, inject } from '@angular/core';
import { AuthService } from '../../services/auth';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin.dashboard',
  imports: [],
  templateUrl: './admin.dashboard.html',
  styleUrl: './admin.dashboard.css',
})
export class AdminDashboard {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  logout(): void {

    this.authService.logout();

    this.router.navigate(['/login']);

  }
}
