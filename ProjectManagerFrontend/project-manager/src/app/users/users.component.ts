import { Component, inject, OnInit } from '@angular/core';
import { UsersService } from './users.service';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';

@Component({
  selector: 'app-users',
  imports: [MatTableModule, MatSortModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {

  displayedColumns = ['name', 'role']
  userSerivce = inject(UsersService);
  users = this.userSerivce.loadedUsers;

  ngOnInit(): void {
    const sub = this.userSerivce.getAllUsers().subscribe();
  }

}
