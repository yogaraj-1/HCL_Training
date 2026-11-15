import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UpperCasePipe } from '@angular/common';
import { Project } from '../../core/models/project.model';
import { HighlightDirective } from '../directives/highlight.directive';

@Component({
  selector: 'app-project-card',
  imports: [UpperCasePipe, HighlightDirective],
  templateUrl: './project-card.html',
  styleUrl: './project-card.css'
})
export class ProjectCardComponent {
  // Parent -> child
  @Input({ required: true }) project!: Project;

  // Child -> parent: emits the project id when "View Details" is clicked
  @Output() viewDetails = new EventEmitter<number>();

  onViewDetails(): void {
    this.viewDetails.emit(this.project.id);
  }
}
