import { Routes } from '@angular/router';


import { Dashboard } from './Components/dashboard/dashboard';
import { AdminDashboard } from './Components/admin.dashboard/admin.dashboard';
import { InternalStaffDashboard } from './Components/internal-staff.dashboard/internal-staff.dashboard';

import { authGuard } from './Guards/auth.guard';
import { guestGuard } from './Guards/guest.guard';
import { roleGuard } from './Guards/role.guard';

import { LoginComponent } from './Components/login-page/login-page';
import { Products } from './Components/products/products';


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
    path: 'admin/products',
    component: Products,
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