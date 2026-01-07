import { Routes, Router, CanMatchFn, RedirectCommand, } from '@angular/router';
import { inject } from '@angular/core';
import { ProjectsComponent } from './projects/projects.component';
import { ProjectDetailsComponent } from './project-details/project-details.component';
import { LoginComponent } from './auth/login/login.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { TasksComponent } from './tasks/tasks.component';
import { UsersComponent } from './users/users.component';
import { AuthService } from './auth/auth.service';

const adminOnlyCanMatch: CanMatchFn = (route, segments) => {

    const router = inject(Router);
    const authService = inject(AuthService);

    if(authService.isAdmin()){
        return true;
    }
    return new RedirectCommand(router.parseUrl('/forbidden'));
}

export const routes: Routes = [
    { path: 'projects', component: ProjectsComponent },
    { path: 'project/:id', component: ProjectDetailsComponent },
    { path: 'tasks', component: TasksComponent },
    { path: 'login', component: LoginComponent },
    { path: 'users', component: UsersComponent, canMatch: [adminOnlyCanMatch]},
    { path: 'forbidden', component: ForbiddenComponent },
];
