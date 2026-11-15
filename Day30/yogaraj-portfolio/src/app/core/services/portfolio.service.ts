import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, delay, map, of } from 'rxjs';
import { Project } from '../models/project.model';
import { Skill } from '../models/skill.model';
import { Experience } from '../models/experience.model';
import { Education } from '../models/education.model';
import { Certification } from '../models/certification.model';
import { CERTIFICATIONS, EDUCATION, EXPERIENCES, PROJECTS, SKILLS } from '../data/portfolio-data';

@Injectable({ providedIn: 'root' })
export class PortfolioService {
  private http = inject(HttpClient);

  /** Flip to true once a backend exists (GET /api/projects, GET /api/skills ...). */
  private useBackend = false;
  private apiBase = '/api';
  private fakeLatencyMs = 400;

  getProjects(): Observable<Project[]> {
    return this.load<Project[]>('/projects', PROJECTS);
  }

  getProjectById(id: number): Observable<Project | undefined> {
    return this.getProjects().pipe(map((projects) => projects.find((p) => p.id === id)));
  }

  getSkills(): Observable<Skill[]> {
    return this.load<Skill[]>('/skills', SKILLS);
  }

  getExperiences(): Observable<Experience[]> {
    return this.load<Experience[]>('/experiences', EXPERIENCES);
  }

  getEducation(): Observable<Education[]> {
    return this.load<Education[]>('/education', EDUCATION);
  }

  getCertifications(): Observable<Certification[]> {
    return this.load<Certification[]>('/certifications', CERTIFICATIONS);
  }

  // Static data now, HttpClient later: only this method changes.
  private load<T>(path: string, fallback: T): Observable<T> {
    if (this.useBackend) {
      return this.http.get<T>(`${this.apiBase}${path}`);
    }
    return of(fallback).pipe(delay(this.fakeLatencyMs));
  }
}
