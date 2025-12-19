import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal, } from '@angular/core';
import { User } from './user.model';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private httpClient = inject(HttpClient);
  private users = signal<User[]>([]);
  loadedUsers = this.users.asReadonly();

  getAllUsers() {
    return this.httpClient.get<User[]>(`https://localhost:7249/api/users`)
      .pipe(
        tap(response => {
          console.log(response)
          this.users.set(response)
        })
      )
  }
}
