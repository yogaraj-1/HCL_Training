import { inject } from '@angular/core';
import {
  CanActivateFn,
  Router
} from '@angular/router';

export const roleGuard = (
  allowedRole: string
): CanActivateFn => {

  return () => {

    const router = inject(Router);

    const token = localStorage.getItem('token');

    if (!token) {
      router.navigate(['/login']);
      return false;
    }

    try {

      const payload = JSON.parse(
        atob(token.split('.')[1])
      );

      const role =
        payload[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ];

      if (role === allowedRole) {
        return true;
      }

      router.navigate(['/login']);

      return false;

    } catch {

      localStorage.removeItem('token');

      router.navigate(['/login']);

      return false;
    }
  };
};