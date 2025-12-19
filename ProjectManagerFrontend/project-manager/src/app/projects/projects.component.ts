import { Component, inject, OnInit, signal, DestroyRef } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { Router } from '@angular/router';
import { ProjectsService } from './projects.service';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-projects',
  imports: [MatTableModule, MatSortModule],
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.css'
})

export class ProjectsComponent implements OnInit {

  private readonly router = inject(Router);
  private projectsService = inject(ProjectsService);
  private authService = inject(AuthService);

  projects = this.projectsService.loadedProjects;

  displayedColumns = ['name', 'status'];

  onProjectClick(id: string) {
    this.router.navigate(['/project', id]);
  }

  ngOnInit(): void {

    const request = this.authService.isAdmin()
      ? this.projectsService.loadAllProjects()
      : this.projectsService.loadProjects();

    request.subscribe({
      error: (error: Error) => {
        console.log(error);
      },
      complete: () => {
        console.log(this.projects());
      },
    });
  }
}

