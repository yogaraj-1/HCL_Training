import { Component, inject } from '@angular/core';
import { PortfolioService } from '../../core/services/portfolio.service';
import { createLoader } from '../../core/utils/loader';

@Component({
  selector: 'app-education',
  templateUrl: './education.html',
  styleUrl: './education.css'
})
export class EducationComponent {
  private portfolio = inject(PortfolioService);
  private educationLoader = createLoader(() => this.portfolio.getEducation());
  private certLoader = createLoader(() => this.portfolio.getCertifications());

  education = this.educationLoader.data;
  certifications = this.certLoader.data;
  loading = this.educationLoader.loading;
  error = this.educationLoader.error;

  retry(): void {
    this.educationLoader.reload();
    this.certLoader.reload();
  }
}
