import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { inject } from '@angular/core';
import { HttpErrorResponse, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { AuthService } from './auth/auth.service';
import { provideAnimations } from '@angular/platform-browser/animations';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, throwError } from 'rxjs';

function logginInterceptor(request: HttpRequest<unknown>, next: HttpHandlerFn) {

  const authService = inject(AuthService);
  const token = authService.getToken();

  if (!token) {
    return next(request);
  }

  let newRequest = request.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  })

  console.log('Outgoing request', newRequest);
  return next(newRequest);
}

function errorInterceptor(request: HttpRequest<unknown>, next: HttpHandlerFn) {

  const snackBar = inject(MatSnackBar);

  return next(request).pipe(
    catchError((err: HttpErrorResponse) => {
      console.log(err);

   let message = err.error?.detail ?? 'Request failed';

    if (err.status === 429) {
      message = 'Too many requests. Try again in a moment.';
    }
      snackBar.open(message, 'Close', {
        duration: 5000,
        panelClass: ['error-snackbar'],
        verticalPosition: 'top',
        horizontalPosition: 'center'
      });
      return throwError(() => err);
    })
  )
}

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
  provideRouter(routes),
  provideAnimations(),
  provideHttpClient(withInterceptors([logginInterceptor, errorInterceptor]))]
};
