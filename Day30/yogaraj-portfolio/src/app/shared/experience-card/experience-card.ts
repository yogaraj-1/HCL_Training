import { Component, Input } from '@angular/core';
import { TitleCasePipe } from '@angular/common';
import { Experience } from '../../core/models/experience.model';
import { HighlightDirective } from '../directives/highlight.directive';

@Component({
  selector: 'app-experience-card',
  imports: [TitleCasePipe, HighlightDirective],
  templateUrl: './experience-card.html',
  styleUrl: './experience-card.css'
})
export class ExperienceCardComponent {
  @Input({ required: true }) experience!: Experience;
}
