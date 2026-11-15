import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { toSignal } from '@angular/core/rxjs-interop';
import { map, switchMap, tap } from 'rxjs';
import { PortfolioService } from '../../core/services/portfolio.service';
import { LoadState, toLoadState } from '../../core/utils/loader';
import { Project } from '../../core/models/project.model';
import { HighlightDirective } from '../../shared/directives/highlight.directive';

@Component({
  selector: 'app-project-details',
  imports: [RouterLink, HighlightDirective],
  templateUrl: './project-details.html',
  styleUrl: './project-details.css'
})
export class ProjectDetailsComponent {
  private route = inject(ActivatedRoute);
  private portfolio = inject(PortfolioService);
  private title = inject(Title);

  // Route parameter -> id -> service -> state. Re-runs if :id changes.
  state = toSignal(
    this.route.paramMap.pipe(
      map((params) => Number(params.get('id'))),
      switchMap((id) => this.portfolio.getProjectById(id)),
      tap((project) => this.title.setTitle(`${project ? project.title : 'Project Not Found'} | Yogaraj P`)),
      toLoadState<Project | undefined>()
    ),
    { initialValue: { status: 'loading' } as LoadState<Project | undefined> }
  );

  get project(): Project | undefined {
    const s = this.state();
    return s.status === 'success' ? s.data : undefined;
  }
}
