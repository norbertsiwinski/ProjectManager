
import { HttpClient } from '@angular/common/http';
import { Injectable, signal, inject, } from '@angular/core';
import { Project } from './project.model';
import { ProjectDetails, ProjectMember } from '../project-details/project-details.model';
import { map, throwError, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {
  private projects = signal<Project[]>([]);
  private projectDetails = signal<ProjectDetails | null>(null);
  private projectMembers = signal<ProjectMember[]>([]);

  private httpClient = inject(HttpClient);

  loadedProjects = this.projects.asReadonly();
  loadedProjectDetails = this.projectDetails.asReadonly();
  loadedProjectMembers = this.projectMembers.asReadonly();

  loadProjects() {
    return this.httpClient
      .get<Project[]>('https://localhost:7249/api/projects/my')
      .pipe(
        tap((loadedProjects) => {
          this.projects.set(loadedProjects)
        })
      );
  }

  loadAllProjects() {
    return this.httpClient
      .get<Project[]>('https://localhost:7249/api/projects/')
      .pipe(
        tap((loadedProjects) => {
          this.projects.set(loadedProjects)
        })
      );
  }

  loadUserProjectDetails(id: string) {
    return this.httpClient
      .get<ProjectDetails>(`https://localhost:7249/api/projects/my/details/${id}`)
      .pipe(
        tap((details) => {
          this.projectDetails.set(details);
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

  addTask(id: string, name: string, projectMemberId: string | null) {
    return this.httpClient
      .post(`https://localhost:7249/api/projects/${id}/taskItems`, { name, projectMemberId })
      .pipe(
    );
  }

  loadProjectMembers(id: string) {
    return this.httpClient
      .get<ProjectMember[]>(`https://localhost:7249/api/projects/projectMembers/${id}`)
      .pipe(
        tap((response) => {
          this.projectMembers.set(response);
        })
      );
  }

  updateTask(id: string, taskId: string, data: {
    name: string | null;
    status: string | null;
    projectMemberId: string | null;
  }) {
    return this.httpClient
      .patch(`https://localhost:7249/api/projects/${id}/taskItems/${taskId}`, data)
      .pipe(
    );
  }
}
