import { Component } from '@angular/core';
import { DatePipe } from '@angular/common';
import { SocialLinksComponent } from '../social-links/social-links';

@Component({
  selector: 'app-footer',
  imports: [DatePipe, SocialLinksComponent],
  templateUrl: './footer.html',
  styleUrl: './footer.css'
})
export class FooterComponent {
  name = 'Yogaraj P';
  today = new Date();
}
