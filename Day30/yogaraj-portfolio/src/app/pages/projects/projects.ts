import { Component, computed, effect, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PortfolioService } from '../../core/services/portfolio.service';
import { createLoader } from '../../core/utils/loader';
import { Project } from '../../core/models/project.model';
import { ProjectCardComponent } from '../../shared/project-card/project-card';

@Component({
  selector: 'app-projects',
  standalone: true,
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

  // 1. Read saved category from sessionStorage on load (fallback to 'All' if none exists)
  selectedCategory = signal(this.getSavedCategory());
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

  constructor() {
    // 2. Effect: Runs whenever selectedCategory() changes
    effect(() => {
      const category = this.selectedCategory();

      // Condition: Remove from storage if 'All' is selected, otherwise save the active category
      if (category === 'All') {
        sessionStorage.removeItem('project_category');
      } else {
        sessionStorage.setItem('project_category', category);
      }
    });
  }

  // Safely validates that the saved value is still a valid category in your list
  private getSavedCategory(): string {
    const saved = sessionStorage.getItem('project_category');
    return saved && this.categories.includes(saved) ? saved : 'All';
  }

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
