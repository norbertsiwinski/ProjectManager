import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { inject } from '@angular/core';
import { HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { AuthService } from './auth/auth.service';
import { provideAnimations } from '@angular/platform-browser/animations'; 

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


export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
  provideRouter(routes),
  provideAnimations(),
  provideHttpClient(withInterceptors([logginInterceptor]))]
};
