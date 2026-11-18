import { Routes } from '@angular/router';

import { LoginComponent } from './pages/login/login';
import { RegisterComponent } from './pages/register/register';
import { StudentDashboardComponent } from './pages/student-dashboard/student-dashboard';
import { StudentProfileComponent } from './pages/student-profile/student-profile';
import { AdminDashboardComponent } from './pages/admin-dashboard/admin-dashboard';

import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

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
    component: StudentDashboardComponent,
    canActivate: [
      authGuard,
      roleGuard('Student')
    ]
  },

  {
    path: 'student/profile',
    component: StudentProfileComponent,
    canActivate: [
      authGuard,
      roleGuard('Student')
    ]
  },

  {
    path: 'admin/dashboard',
    component: AdminDashboardComponent,
    canActivate: [
      authGuard,
      roleGuard('Admin')
    ]
  },

  {
    path: '**',
    redirectTo: 'login'
  }

];