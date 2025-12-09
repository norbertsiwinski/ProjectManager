import { Routes } from '@angular/router';
import { ProjectsComponent } from './projects/projects.component';
import { ProjectDetailsComponent } from './project-details/project-details.component';

export const routes: Routes = [
    { path: 'projects', component: ProjectsComponent },
    { path: 'project/:id', component: ProjectDetailsComponent },
    { path: 'tasks', redirectTo: 'projects', pathMatch: 'full' },
    { path: 'users', redirectTo: 'projects', pathMatch: 'full' },
];
