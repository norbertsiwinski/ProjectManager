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
import { MatDialog } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { AuthService } from '../auth/auth.service';
import { TaskDialogComponent } from '../task-dialog/task-dialog.component';

@Component({
  selector: 'app-project-details',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatSelectModule,
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

  projectId: string | null = '';

  form = new FormGroup({
    taskName: new FormControl('', { nonNullable: true, validators: [] }),
    assigneeId: new FormControl<string | null>(null)
  });

  private authService = inject(AuthService);
  private route = inject(ActivatedRoute);
  private projectsService = inject(ProjectsService);
  private dialog = inject(MatDialog);

  projectDetails = this.projectsService.loadedProjectDetails;
  projectMembers = this.projectsService.loadedProjectMembers;

  taskColumns = ['name', 'assigneeName', 'status'];
  memberColumns = ['email', 'role'];

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id');

    if (!this.projectId) {
      console.error("Id is missing!");
      return;
    }

    const request = this.authService.isAdmin()
      ? this.projectsService.loadProjectDetails(this.projectId)
      : this.projectsService.loadUserProjectDetails(this.projectId);

    request.subscribe({
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
    const projectMemberId = this.form.controls.assigneeId.value;

    this.projectsService.addTask(projectId, taskName, projectMemberId)
      .subscribe({
        next: () => {
          this.projectsService.loadProjectDetails(projectId).subscribe();
        },
        error: (error: Error) => {
          console.log(error);
        }
      })
  }

  openTaskDialog(task: any) {
    const dialogRef = this.dialog.open(TaskDialogComponent, {
      width: '520px',
      height: '600px',
      data: {
        task,
        projectId: this.projectId,
      },
      autoFocus: false
    });
  }
}
