import { Component, inject } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterLink } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navigation-bar',
  standalone: true,
  templateUrl: './navigation-bar.component.html',
  styleUrl: './navigation-bar.component.css',
  imports: [MatToolbarModule, MatIcon, RouterLink, CommonModule]
})
export class NavigationBarComponent {

  authService = inject(AuthService);

  get isAdmin(): boolean {
    return this.authService.isAdmin();
  }
}
