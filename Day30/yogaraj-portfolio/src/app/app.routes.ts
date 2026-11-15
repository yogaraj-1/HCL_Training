import { Routes } from '@angular/router';

// loadComponent = lazy loading: each page is downloaded only when first visited.
export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', title: 'Home | Yogaraj P', loadComponent: () => import('./pages/home/home').then((m) => m.HomeComponent) },
  { path: 'about', title: 'About | Yogaraj P', loadComponent: () => import('./pages/about/about').then((m) => m.AboutComponent) },
  { path: 'skills', title: 'Skills | Yogaraj P', loadComponent: () => import('./pages/skills/skills').then((m) => m.SkillsComponent) },
  { path: 'projects', title: 'Projects | Yogaraj P', loadComponent: () => import('./pages/projects/projects').then((m) => m.ProjectsComponent) },
  { path: 'projects/:id', title: 'Project Details | Yogaraj P', loadComponent: () => import('./pages/project-details/project-details').then((m) => m.ProjectDetailsComponent) },
  { path: 'experience', title: 'Experience | Yogaraj P', loadComponent: () => import('./pages/experience/experience').then((m) => m.ExperienceComponent) },
  { path: 'education', title: 'Education | Yogaraj P', loadComponent: () => import('./pages/education/education').then((m) => m.EducationComponent) },
  { path: 'contact', title: 'Contact | Yogaraj P', loadComponent: () => import('./pages/contact/contact').then((m) => m.ContactComponent) },
  { path: '**', redirectTo: 'home' }
];
