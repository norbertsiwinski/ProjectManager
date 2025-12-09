
import { HttpClient } from '@angular/common/http';
import { Injectable, signal, inject, } from '@angular/core';
import { Project } from './project.model';
import { ProjectDetails } from '../project-details/project-details.model';
import { catchError, map, throwError, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {
  private projects = signal<Project[]>([]);
private projectDetails = signal<ProjectDetails | null>(null);
  private httpClient = inject(HttpClient);

  loadedProjects = this.projects.asReadonly();
  loadedProjectDetails = this.projectDetails.asReadonly();

  loadProjects() {
    return this.httpClient
      .get<Project[]>('https://localhost:7249/api/projects')
      .pipe(
        tap((loadedProjects) => {
          this.projects.set(loadedProjects)
        })
      );
  }

  loadProjectDetails(id: string) {
    return this.httpClient
      .get<ProjectDetails>(`https://localhost:7249/api/projects/details/${id}`)
      .pipe(
        tap((details) => {
          this.projectDetails.set(details);
        })
      );
  }
}
