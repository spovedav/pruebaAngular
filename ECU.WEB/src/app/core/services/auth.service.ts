import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { _urlApiAuth } from '../../endpoint/const';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private LOGIN_URL = _urlApiAuth + 'auth';
  private tokenKey = 'authToken';

  private REFRESH_URL = _urlApiAuth + 'refresh';
  private refreshTokenKey = 'authTokenRefresh';

  constructor(private httpClient: HttpClient, private router: Router) { }

  login(user: string, password: string): Observable<any>{

    const basicAuth = btoa(`${user}:${password}`); // Codificar en Base64

    const headers = new HttpHeaders({
      Authorization: `Basic ${basicAuth}`,
    });

    return this.httpClient.get<any>(this.LOGIN_URL, { headers }).pipe(
      tap(response => {

        if (!response.tieneError) {
          this.setToken(response.result.token);
          //LO PUEDO COLOCAR PERO PARA ESTE PROYECTO ESTA DE MÁS
          //this.setRefreshToken(response.refreshToken)
          //this.autoRefreshToken();
        }
      })
    )
  }

  private setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  } 

  //se que esta en publico,, :v
  public getToken(): string | null {
    if(typeof window !== 'undefined'){
      return localStorage.getItem(this.tokenKey);
    }else {
      return null;
    }
  }

  private setRefreshToken(token: string): void {
    localStorage.setItem(this.refreshTokenKey, token);
  } 

  private getRefreshToken(): string | null {
    if(typeof window !== 'undefined'){
      return localStorage.getItem(this.refreshTokenKey);
    }else {
      return null;
    }
  }

  refreshToken(): Observable<any>{
    const refreshToken  = this.getRefreshToken()
    return this.httpClient.post<any>(this.REFRESH_URL, {refreshToken}).pipe(
      tap(response => {
        if(response.token){
          console.log(response.token);
          this.setToken(response.token);
          this.setRefreshToken(response.refreshToken)
          this.autoRefreshToken()
        }
      })
    )
  }

  autoRefreshToken(): void {
    const token = this.getToken();
    if(!token){
      return;
    }
    const payload = JSON.parse(atob(token.split('.')[1]));
    const exp = payload.exp * 1000;

    const timeout = exp - Date.now() - (60 * 1000);

    setTimeout(() => {
      this.refreshToken().subscribe()
    }, timeout);
   
  }


  isAuthenticated(): boolean {
    const token = this.getToken();
    if(!token){
      return false;
    }
    const payload = JSON.parse(atob(token.split('.')[1]));

    const _exp = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/expired'];

    const inputDate = new Date(_exp);
    const fecha = new Date();
    return inputDate >= fecha;
  }

  logout(): void{
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.refreshTokenKey);
    this.router.navigate(['/login']);
  }
}
