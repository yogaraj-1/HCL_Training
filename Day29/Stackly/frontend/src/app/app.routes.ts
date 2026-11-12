import { Routes } from '@angular/router';
import { StudentList } from './components/student-list/student-list';
import { StudentRegister } from './components/student-register/student-register';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'student-list',
    pathMatch: 'full'
  },
  {
    path: 'student-list',
    component: StudentList
  },
  {
    path: 'register',
    component: StudentRegister
  }
];