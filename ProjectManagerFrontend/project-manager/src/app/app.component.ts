import { Component, inject } from '@angular/core';
import { NavigationBarComponent } from "./navigation-bar/navigation-bar.component";
import { RouterOutlet } from "@angular/router";
import { CommonModule } from '@angular/common';  
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  imports: [NavigationBarComponent, RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'project-manager';

  authService = inject(AuthService);

  showNavbar() {
    return this.authService.isLoggedIn();
  }
}
