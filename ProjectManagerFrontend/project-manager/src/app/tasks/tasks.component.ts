import { Component, inject, OnInit } from '@angular/core';
import { TasksService } from './tasks.service';
import { MatSortModule, } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tasks',
  imports: [MatTableModule, MatSortModule, CommonModule],
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.css'
})
export class TasksComponent implements OnInit {

  private tasksService = inject(TasksService);
  tasks = this.tasksService.loadedTasks;

  displayedColumns = ['name', 'assigneeName', 'status'];

  ngOnInit(): void {
    const sub = this.tasksService.getTaskItemsForUser()
      .subscribe({
        error: (error: Error) => {
          console.log(error);
        },
      });
  }
}
