// auth.interceptor.ts
import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authReq = req;
    const token = this.authService.getToken();

    if (token) {
      const clonedRequest = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });

      return next.handle(clonedRequest);
    } else {
      const username = 'admin';  // Reemplazar con tu lógica
      const password = 'admin';  // Reemplazar con tu lógica
      const basicAuth = 'Basic ' + btoa(username + ':' + password); // Codificar las credenciales

      // Si no hay token, agregar Basic Auth
      authReq = req.clone({
        setHeaders: {
          Authorization: basicAuth,
        },
      });
    }

    return next.handle(req);
  }
}
