import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginDTO } from '../DTOs/login.dto';
import { Observable, tap } from 'rxjs';
import { Token } from '../models/token.model';
import { CookieService } from 'ngx-cookie-service';
import { JwtHelperService } from '@auth0/angular-jwt';

export const ACCESS_TOKEN_KEY = "access_token";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = 'http://localhost:5032'; // URL to bakcend API

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    private jwtHelper: JwtHelperService
  ) {}

  login(data: LoginDTO): Observable<Token> {
    // Send a POST request to API with the login data
    return this.http.post<Token>(this.apiUrl + '/api/authorization', data).pipe(
      tap(token => {
        // Store the access token in a cookies
        this.cookieService.set(ACCESS_TOKEN_KEY, token.access_token, undefined, '/');
      })
    );
  }

  isAuthenticated(): boolean {
    const token = this.cookieService.get(ACCESS_TOKEN_KEY); // Get the access token from cookies 
    return token !== null && !this.jwtHelper.isTokenExpired(token); // Check if the token is expired
  }

  extractUsername(): string {
    const token = this.cookieService.get(ACCESS_TOKEN_KEY); // Get the access token from cookies
    const decodedToken = this.jwtHelper.decodeToken(token); // Decode the token
    return decodedToken['name']; // Extract the username from the token
  }

  logout(): void {
    this.cookieService.delete(ACCESS_TOKEN_KEY, '/'); // Delete the access token from cookies
  }
}
