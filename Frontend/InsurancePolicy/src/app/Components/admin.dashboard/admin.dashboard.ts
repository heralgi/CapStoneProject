import { Component, inject } from '@angular/core';
import { AuthService } from '../../services/auth';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-admin.dashboard',
  imports: [RouterOutlet],
  templateUrl: './admin.dashboard.html',
  styleUrl: './admin.dashboard.css',
})
export class AdminDashboard {
  private readonly authService = inject(AuthService);
  readonly router = inject(Router);



  logout(): void {

    this.authService.logout();

    this.router.navigate(['/login']);

  }
}
