import { Component, inject } from '@angular/core';
import { AuthService } from '../../services/auth';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-internal-staff.dashboard',
  imports: [RouterOutlet],
  templateUrl: './internal-staff.dashboard.html',
  styleUrl: './internal-staff.dashboard.css',
})
export class InternalStaffDashboard {

  private readonly authService = inject(AuthService);
  readonly router = inject(Router);

  logout(): void {

    this.authService.logout();

    this.router.navigate(['/login']);

  }
}
