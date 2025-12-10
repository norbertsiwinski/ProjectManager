import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

interface LoginResponse {
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private httpService = inject(HttpClient);
  private readonly TOKEN_KEY = 'access_token';


  login(email: string, password: string){

   return this.httpService.post<LoginResponse>('https://localhost:7249/api/users/login', {
      email, password
    }).pipe(
      tap(response => {
        this.setToken(response.token)})
    )
  }

  logout(){
    this.removeToken();
  }

  getToken(){
    return localStorage.getItem(this.TOKEN_KEY);
  }

   isLoggedIn() {
    const token = this.getToken();

    return !!token;
  }

  private setToken(token: string){
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  private removeToken(){
    localStorage.removeItem(this.TOKEN_KEY);
  }
}
