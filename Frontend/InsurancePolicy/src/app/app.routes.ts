import { Routes } from '@angular/router';


import { Dashboard } from './Components/dashboard/dashboard';
import { AdminDashboard } from './Components/admin.dashboard/admin.dashboard';
import { InternalStaffDashboard } from './Components/internal-staff.dashboard/internal-staff.dashboard';

import { authGuard } from './Guards/auth.guard';
import { guestGuard } from './Guards/guest.guard';
import { roleGuard } from './Guards/role.guard';

import { LoginComponent } from './Components/login-page/login-page';
import { Products } from './Components/products/products';
import { Plan } from './Components/plan/plan';
import { Policy } from './Components/policy/policy';
import { Claim } from './Components/claim/claim';
import { UserComponent } from './Components/user-component/user-component';
import { ProductComponent } from './Components/customer/product-component/product-component';
import { PlanCustomer } from './Components/customer/plan-customer/plan-customer';
import { PolicyCustomer } from './Components/customer/policy-customer/policy-customer';
import { Payment } from './Components/payment/payment';


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
    children: [
        { path: '', redirectTo: 'products', pathMatch: 'full' },
        { path: 'products', component: Products },
        { path: 'plans', component: Plan },
        { path: 'policy', component: Policy },
        { path: 'claim', component: Claim},
        { path: 'user', component: UserComponent},
    ],
    canActivate: [authGuard, roleGuard(['Admin'])]
},
{
    path: 'staff/dashboard',
    component: InternalStaffDashboard,
    children: [
        { path: '', redirectTo: 'policy', pathMatch: 'full' },
        { path: 'plans', component: Plan },
        { path: 'policy', component: Policy },
        { path: 'claim', component: Claim},
        { path: 'payment', component: Payment},
    ],
    canActivate: [authGuard, roleGuard(['InternalStaff'])]
},
{
    path: 'dashboard',
    component: Dashboard,
    children: [
      { path: '', redirectTo: 'products', pathMatch: 'full'},
      { path: 'products', component: ProductComponent},
      { path: 'plans/:id', component: PlanCustomer},
      { path: 'policy', component: PolicyCustomer},
    ],
    canActivate: [authGuard, roleGuard(['Customer'])]
},

  {
    path: '**',
    redirectTo: 'dashboard'
  }

];