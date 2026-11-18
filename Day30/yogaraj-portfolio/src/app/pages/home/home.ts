import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { SocialLinksComponent } from '../../shared/social-links/social-links';
import { StatsComponent } from '../../shared/stats/stats';
import { Stat } from '../../core/models/stat.model';

@Component({
  selector: 'app-home',
  imports: [RouterLink, SocialLinksComponent, StatsComponent],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class HomeComponent {
  private router = inject(Router);

  name = 'Yogaraj P';
  role = 'Full Stack Developer';
  intro =
    'I build modern web applications using Angular, React, Node.js, Java, .NET, MongoDB and other modern technologies.';
  photoUrl = 'images/yogaraj.jpg';
  photoAlt = 'Portrait of Yogaraj P';

  stats: Stat[] = [
    { id: 1, value: '8.7', label: 'CGPA' },
    { id: 2, value: '250+', label: 'LeetCode Problems' },
    { id: 3, value: '1st', label: 'Department Coding Event' },
    { id: 4, value: '10+', label: 'Projects' }
  ];

  // Event binding: called from (click) in home.html
  goTo(path: string): void {
    this.router.navigate([path]);
  }
}
