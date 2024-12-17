import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { provideClientHydration } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, provideHttpClient, withFetch } from '@angular/common/http';
import { routes } from './app-routing.module';
import { AuthInterceptor } from './core/services/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection(
      {
        eventCoalescing: true
      }), 
    provideRouter(routes), 
    provideClientHydration(),
    provideHttpClient(withFetch(),),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor, // Clase de tu interceptor
      multi: true, // Esto es necesario para que puedas tener múltiples interceptores
    }
  ]
};

// Registra el interceptor aquí
//{
//provide: HTTP_INTERCEPTORS,
//useClass: AuthInterceptor, // Clase de tu interceptor
//multi: true, // Esto es necesario para que puedas tener múltiples interceptores
//}
