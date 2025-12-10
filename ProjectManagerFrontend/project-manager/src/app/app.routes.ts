import { Routes } from '@angular/router';
import { ProjectsComponent } from './projects/projects.component';
import { ProjectDetailsComponent } from './project-details/project-details.component';
import { LoginComponent } from './auth/login/login.component';

export const routes: Routes = [
    { path: 'projects', component: ProjectsComponent },
    { path: 'project/:id', component: ProjectDetailsComponent },
    { path: 'tasks', redirectTo: 'projects', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
];
