import { Component } from '@angular/core';
import { SOCIAL_LINKS } from '../../core/data/social-links';
import { SocialLink } from '../../core/models/social-link.model';

@Component({
  selector: 'app-social-links',
  templateUrl: './social-links.html',
  styleUrl: './social-links.css'
})
export class SocialLinksComponent {
  links: SocialLink[] = SOCIAL_LINKS;
}
