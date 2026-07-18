import { Routes } from '@angular/router';

import { LoginComponent } from './Components/login-page/login-page';
import { Dashboard } from './Components/dashboard/dashboard';

import { authGuard } from './Guards/auth.guard';
import { guestGuard } from './Guards/guest.guard';

export const routes: Routes = [

  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },

  {
    path: 'login',
    component: LoginComponent,
    canActivate: [guestGuard]
  },

  {
    path: 'dashboard',
    component: Dashboard,
    canActivate: [authGuard]
  },

  {
    path: '**',
    redirectTo: 'dashboard'
  }

];