import { Component, inject } from '@angular/core';
import { PortfolioService } from '../../core/services/portfolio.service';
import { createLoader } from '../../core/utils/loader';
import { ExperienceCardComponent } from '../../shared/experience-card/experience-card';

@Component({
  selector: 'app-experience',
  imports: [ExperienceCardComponent],
  templateUrl: './experience.html',
  styleUrl: './experience.css'
})
export class ExperienceComponent {
  private portfolio = inject(PortfolioService);
  private loader = createLoader(() => this.portfolio.getExperiences());

  experiences = this.loader.data;
  loading = this.loader.loading;
  error = this.loader.error;

  retry(): void {
    this.loader.reload();
  }
}
