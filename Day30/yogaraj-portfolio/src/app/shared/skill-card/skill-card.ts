import { Component, Input } from '@angular/core';
import { Skill } from '../../core/models/skill.model';
import { SkillLevelPipe } from '../pipes/skill-level.pipe';
import { HighlightDirective } from '../directives/highlight.directive';

@Component({
  selector: 'app-skill-card',
  imports: [SkillLevelPipe, HighlightDirective],
  templateUrl: './skill-card.html',
  styleUrl: './skill-card.css'
})
export class SkillCardComponent {
  @Input({ required: true }) skill!: Skill;
}
