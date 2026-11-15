import { Component, computed, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PortfolioService } from '../../core/services/portfolio.service';
import { createLoader } from '../../core/utils/loader';
import { Project } from '../../core/models/project.model';
import { ProjectCardComponent } from '../../shared/project-card/project-card';

@Component({
  selector: 'app-projects',
  imports: [FormsModule, ProjectCardComponent],
  templateUrl: './projects.html',
  styleUrl: './projects.css'
})
export class ProjectsComponent {
  private portfolio = inject(PortfolioService);
  private router = inject(Router);
  private loader = createLoader(() => this.portfolio.getProjects());

  loading = this.loader.loading;
  error = this.loader.error;

  categories = ['All', 'Frontend', 'Backend', 'Full Stack', 'AI', '.NET'];
  selectedCategory = signal('All');
  searchTerm = signal(''); // bound with [(ngModel)]

  // Search and category filter work together
  filteredProjects = computed<Project[]>(() => {
    const projects = this.loader.data() ?? [];
    const term = this.searchTerm().trim().toLowerCase();
    const category = this.selectedCategory();

    return projects.filter((p) => {
      const matchesCategory = category === 'All' || p.category === category;
      const matchesSearch =
        term === '' ||
        p.title.toLowerCase().includes(term) ||
        p.category.toLowerCase().includes(term) ||
        p.technologies.some((t) => t.toLowerCase().includes(term));
      return matchesCategory && matchesSearch;
    });
  });

  selectCategory(category: string): void {
    this.selectedCategory.set(category);
  }

  // Receives the id emitted by <app-project-card (viewDetails)="...">
  openProject(id: number): void {
    this.router.navigate(['/projects', id]);
  }

  retry(): void {
    this.loader.reload();
  }
}
