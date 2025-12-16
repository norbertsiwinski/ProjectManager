import { inject, Injectable, signal, } from '@angular/core';
import { Task } from './task.model';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  private httpClient = inject(HttpClient);

  private tasks = signal<Task[]>([]);
  loadedTasks = this.tasks.asReadonly();

  getTaskItemsForUser(){
    return this.httpClient.get<Task[]>('https://localhost:7249/api/projects/my/taskItems')
    .pipe(
      tap((loadedTasks) => {
        this.tasks.set(loadedTasks)
      })
    );
  }
}
