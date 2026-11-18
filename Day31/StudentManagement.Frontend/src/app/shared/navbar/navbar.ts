import {
  Component,
  OnInit,
  OnDestroy,
  ChangeDetectorRef
} from '@angular/core';

import {
  Router,
  RouterLink,
  NavigationEnd
} from '@angular/router';

import { Subscription, filter } from 'rxjs';

import { AuthService } from '../../core/services/auth';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class NavbarComponent
  implements OnInit, OnDestroy {

  loggedIn = false;
  role: string | null = null;

  private subscriptions =
    new Subscription();

  constructor(
  private authService: AuthService,
  private router: Router,
  private cdr: ChangeDetectorRef
) {}

  ngOnInit(): void {

    // Initial state
    this.updateNavbar();

    // Update navbar whenever navigation happens
    this.subscriptions.add(

      this.router.events
        .pipe(
          filter(
            event => event instanceof NavigationEnd
          )
        )
        .subscribe(() => {

          this.updateNavbar();

        })

    );

    // Also listen for login/logout changes
    this.subscriptions.add(

      this.authService.loggedIn$
        .subscribe(() => {

          this.updateNavbar();

        })

    );

    this.subscriptions.add(

      this.authService.role$
        .subscribe(() => {

          this.updateNavbar();

        })

    );
  }

updateNavbar(): void {

  this.loggedIn =
    this.authService.isLoggedIn();

  this.role =
    this.authService.getRole();

  console.log(
    'NAVBAR UPDATED:',
    this.loggedIn,
    this.role
  );

  this.cdr.detectChanges();
}

  logout(): void {

    this.authService.logout();

    this.router.navigate(['/login']);
  }

  ngOnDestroy(): void {

    this.subscriptions.unsubscribe();

  }
}