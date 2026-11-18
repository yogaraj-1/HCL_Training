import { Routes } from '@angular/router';

import { LoginComponent } from './pages/login/login';
import { RegisterComponent } from './pages/register/register';
import { StudentDashboardComponent } from './pages/student-dashboard/student-dashboard';
import { StudentProfileComponent } from './pages/student-profile/student-profile';
import { AdminDashboardComponent } from './pages/admin-dashboard/admin-dashboard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },

  {
    path: 'login',
    component: LoginComponent
  },

  {
    path: 'register',
    component: RegisterComponent
  },

  {
    path: 'student/dashboard',
    component: StudentDashboardComponent
  },

  {
    path: 'student/profile',
    component: StudentProfileComponent
  },

  {
    path: 'admin/dashboard',
    component: AdminDashboardComponent
  },

  {
    path: '**',
    redirectTo: 'login'
  }
];