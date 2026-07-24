import { Component, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {

  private readonly authService = inject(AuthService);
  readonly router = inject(Router);

  logout(): void {

    this.authService.logout();

    this.router.navigate(['/login']);

  }

}