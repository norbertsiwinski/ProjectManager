import { Component, inject, OnInit, signal, DestroyRef } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { Router } from '@angular/router';
import { ProjectsService } from './projects.service';

@Component({
  selector: 'app-projects',
  imports: [MatTableModule, MatSortModule],
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.css'
})

export class ProjectsComponent implements OnInit {

  private readonly router = inject(Router);
  private placesService = inject(ProjectsService);
  projects = this.placesService.loadedProjects;

  displayedColumns = ['name', 'status'];

  onProjectClick(id: string) {
    this.router.navigate(['/project', id]);
  }

  ngOnInit(): void {

    const subs = this.placesService.loadProjects()
      .subscribe({
        error: (error: Error) => {
          console.log(error);
        },
        complete: () => {
          console.log(this.projects());
        },
      });
  }
}
