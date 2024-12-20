import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginDTO } from '../DTOs/login.dto';
import { Observable, tap } from 'rxjs';
import { Token } from '../models/token.model';
import { CookieService } from 'ngx-cookie-service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

export const ACCESS_TOKEN_KEY = "access_token";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = 'http://localhost:5032'; // URL to bakcend API

  constructor(
    private router: Router,
    private http: HttpClient,
    private cookieService: CookieService,
    private jwtHelper: JwtHelperService
  ) {}

  login(data: LoginDTO): Observable<Token> {
    return this.http.post<Token>(this.apiUrl + '/api/authorization', data).pipe(
      tap(token => {
        this.cookieService.set(ACCESS_TOKEN_KEY, token.access_token);
      })
    );
  }

  isAuthenticated(): boolean {
    const token = this.cookieService.get(ACCESS_TOKEN_KEY);
    return token !== null && this.jwtHelper.isTokenExpired(token);
  }

  logout(): void {
    this.cookieService.delete(ACCESS_TOKEN_KEY, '/auth');
    this.router.navigate(['/auth/login']);
  }
}
