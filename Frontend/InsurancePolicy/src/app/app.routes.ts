import { Routes } from '@angular/router';
import { Dashboard } from './Components/dashboard/dashboard';
import { LoginComponent } from './Components/login-page/login-page'; 
import { Register } from './Components/register/register';

export const routes: Routes = [
    { path: '', component: LoginComponent, title: 'InsurancePolicy' },
    { path: 'dashboard', component: Dashboard, title: 'InsurancePolicy — Dashboard' },
    { path: 'register', component: Register, title: 'Insurance — Register' }
];
