import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    loadComponent: () =>
      import('./features/home/home')
        .then(m => m.Home)
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./features/auth/login/login')
        .then(m => m.Login)
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./features/auth/register/register')
        .then(m => m.Register)
  },
  {
    path: 'movies',
    loadComponent: () =>
      import('./features/movies/movie-list/movie-list')
        .then(m => m.MovieList)
  },
  {
    path: 'movies/:id',
    loadComponent: () =>
      import('./features/movies/movie-details/movie-details')
        .then(m => m.MovieDetails)
  },
  {
    path: 'my-bookings',
    loadComponent: () =>
      import('./features/bookings/my-bookings/my-bookings')
        .then(m => m.MyBookings)
  },
  {
    path: 'admin',
    loadComponent: () =>
      import('./features/admin/admin-dashboard/admin-dashboard')
        .then(m => m.AdminDashboard)
  },
  {
  path: 'seat-selection',
  loadComponent: () =>
    import('./features/seats/seat-selection/seat-selection')
      .then(m => m.SeatSelection)
},
{
  path: 'booking-summary',
  loadComponent: () =>
    import('./features/bookings/booking-summary/booking-summary')
      .then(m => m.BookingSummary)
},
{
    path: '**',
    redirectTo: 'home'
  }
];