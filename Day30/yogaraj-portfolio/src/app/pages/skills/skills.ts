import { Component, computed, inject, signal } from '@angular/core';
import { PortfolioService } from '../../core/services/portfolio.service';
import { createLoader } from '../../core/utils/loader';
import { Skill } from '../../core/models/skill.model';
import { SkillCardComponent } from '../../shared/skill-card/skill-card';

@Component({
  selector: 'app-skills',
  imports: [SkillCardComponent],
  templateUrl: './skills.html',
  styleUrl: './skills.css'
})
export class SkillsComponent {
  private portfolio = inject(PortfolioService);
  private loader = createLoader(() => this.portfolio.getSkills());

  loading = this.loader.loading;
  error = this.loader.error;

  categories = ['All', 'Frontend', 'Backend', 'Database', 'Programming', 'Tools'];
  selectedCategory = signal('All');

  filteredSkills = computed<Skill[]>(() => {
    const skills = this.loader.data() ?? [];
    const category = this.selectedCategory();
    if (category !== 'All') {
      return skills.filter((s) => s.category === category);
    }
    // "All": JavaScript is listed under two categories, show it once
    return [...new Map(skills.map((s) => [s.name, s])).values()];
  });

  filterSkills(category: string): void {
    this.selectedCategory.set(category);
  }

  retry(): void {
    this.loader.reload();
  }
}
