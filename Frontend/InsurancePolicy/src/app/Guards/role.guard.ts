import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

import { AuthService } from '../services/auth';

export const roleGuard = (allowedRoles: string[]): CanActivateFn => {

  return () => {

    const authService = inject(AuthService);
    const router = inject(Router);

    // Not logged in
    if (!authService.isLoggedIn()) {

      router.navigate(['/login']);

      return false;

    }

    // Logged in user's role
    const role = authService.getRole();

    // Role allowed
    if (role && allowedRoles.includes(role)) {

      return true;

    }

    // Logged in but wrong role
    router.navigateByUrl(authService.getDashboardRoute());

    return false;

  };

};