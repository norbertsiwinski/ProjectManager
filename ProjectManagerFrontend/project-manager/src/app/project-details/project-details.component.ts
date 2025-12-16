import { Component, inject, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { ProjectsService } from '../projects/projects.service';
import { CommonModule } from '@angular/common';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-project-details',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './project-details.component.html',
  styleUrl: './project-details.component.css'
})

export class ProjectDetailsComponent implements OnInit {

  form = new FormGroup({
    taskName: new FormControl('', {
      nonNullable: true
    })
  });

  private route = inject(ActivatedRoute);
  private projectsService = inject(ProjectsService);

  projectDetails = this.projectsService.loadedProjectDetails;

  taskColumns = ['name', 'assigneeName', 'status'];
  memberColumns = ['email', 'role'];

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (!id) {
      console.error("Id is missing!");
      return;
    }
    const sub = this.projectsService.loadProjectDetails(id)
      .subscribe({
        error: (error: Error) => {
          console.log(error);
        },
        complete: () => {
          console.log(this.projectDetails());
        },
      });
  }

  addTask(projectId: string) {

    if (this.form.invalid) {
      return;
    }
    const taskName = this.form.controls.taskName.value;

    const sub = this.projectsService.addTask(projectId, taskName)
      .subscribe({
        next: () => {
          this.projectsService.loadProjectDetails(projectId).subscribe();
        },
        error: (error: Error) => {
          console.log(error);
        }
      })
  }

}
