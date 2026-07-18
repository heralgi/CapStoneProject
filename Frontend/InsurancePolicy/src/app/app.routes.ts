import { Routes } from '@angular/router';

import { LoginComponent } from './Components/login-page/login-page';
import { Dashboard } from './Components/dashboard/dashboard';

import { authGuard } from './Guards/auth.guard';
import { guestGuard } from './Guards/guest.guard';
import { AdminDashboard } from './Components/admin.dashboard/admin.dashboard';
import { InternalStaffDashboard } from './Components/internal-staff.dashboard/internal-staff.dashboard';
import { roleGuard } from './Guards/role.guard';

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
    path: 'admin/dashboard',
    component: AdminDashboard,
    canActivate: [authGuard, roleGuard(['Admin'])]
},
{
    path: 'staff/dashboard',
    component: InternalStaffDashboard,
    canActivate: [authGuard, roleGuard(['InternalStaff'])]
},
{
    path: 'dashboard',
    component: Dashboard,
    canActivate: [authGuard, roleGuard(['Customer'])]
},

  {
    path: '**',
    redirectTo: 'dashboard'
  }

];