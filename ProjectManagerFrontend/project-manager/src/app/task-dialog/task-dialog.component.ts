import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, } from '@angular/material/dialog';
import { TaskItem } from '../project-details/project-details.model';
import { FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { FormControl } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatOption } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { ProjectsService } from '../projects/projects.service';
import { MatButtonModule } from '@angular/material/button';

export interface TaskDialogData {
  task: TaskItem,
  projectId: string,
}

@Component({
  selector: 'app-task-dialog',
  standalone: true,
  imports: [
    MatDialogModule,
    CommonModule,
    MatOption,
    MatButtonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
  ],
  templateUrl: './task-dialog.component.html',
  styleUrl: './task-dialog.component.css'
})

export class TaskDialogComponent implements OnInit {

  private projectService = inject(ProjectsService);

  projectMembers = this.projectService.loadedProjectMembers;
  statuses = ['New', 'InProgress', 'Completed'];

  form = new FormGroup({
    name: new FormControl('', { nonNullable: true }),
    status: new FormControl('', { nonNullable: true }),
    assigneeId: new FormControl('')
  });

  data = inject<TaskDialogData>(MAT_DIALOG_DATA);

  constructor() {
  }

  ngOnInit(): void {

    this.projectService.loadProjectMembers(this.data.projectId)
      .subscribe();

    this.form.patchValue({
      name: this.data.task.name,
      status: this.data.task.status,
      assigneeId: this.data.task.id
    })
  }

  updateTask() {

    if (this.form.invalid) {
      return;
    }

    const data = {
      name: this.form.value.name || null,
      status: this.form.value.status || null,
      projectMemberId: this.form.value.assigneeId || null,
    };

    console.log('project member id' + data.projectMemberId);

    this.projectService.updateTask(this.data.projectId, this.data.task.id, data)
      .subscribe({
        next: () => {
          this.projectService.loadProjectDetails(this.data.projectId).subscribe();
        },
        error: (error: Error) => {
          console.log(error);
        }
      })
  }
}
