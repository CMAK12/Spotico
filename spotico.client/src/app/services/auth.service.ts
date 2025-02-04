import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginDTO } from '../DTOs/login.dto';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Token } from '../models/token.model';
import { CookieService } from 'ngx-cookie-service';
import { JwtHelperService } from '@auth0/angular-jwt';

export const ACCESS_TOKEN_KEY = 'access_token';
const API_URL = 'http://localhost:5032/api/authorization';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private cookieService = inject(CookieService);
  private jwtHelper = inject(JwtHelperService);

  // Observable to notify the subscribers if the user is authenticated
  private isAuthenticate: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(this.isAuthenticated());
  authStatus$ = this.isAuthenticate.asObservable();

  login(data: LoginDTO): Observable<Token> {
    // Send a POST request to API with the login data
    return this.http.post<Token>(API_URL, data).pipe(
      tap({
        next: (token) => {
          // Store the access token in a cookies
          this.cookieService.set(
            ACCESS_TOKEN_KEY,
            token.access_token,
            undefined,
            '/',
          );
          this.isAuthenticate.next(true); // Notify the subscribers that the user is authenticated
        },
        error: (error) => {
          console.error('Failed to login', error);
        },
      }),
    );
  }

  extractUserId(): string {
    const token = this.cookieService.get(ACCESS_TOKEN_KEY); // Get the token from cookies
    const decodedToken = this.jwtHelper.decodeToken(token); // Decode the token
    if (token != null && !this.jwtHelper.isTokenExpired(token)) {
      return decodedToken['sub']; // Extract the user ID from the token
    }
    return null;
  }

  extractUsername(): string {
    const token = this.cookieService.get(ACCESS_TOKEN_KEY); // Get the access token from cookies
    const decodedToken = this.jwtHelper.decodeToken(token); // Decode the token
    if (token != null && !this.jwtHelper.isTokenExpired(token)) {
      return decodedToken['name']; // Extract the username from the token
    }
    return null;
  }

  getToken(): string {
    if (this.authStatus$)
      return this.cookieService.get(ACCESS_TOKEN_KEY); // Get the access token from cookies
    else return null;
  }

  logout(): void {
    this.cookieService.delete(ACCESS_TOKEN_KEY, '/'); // Delete the access token from cookies
    this.isAuthenticate.next(false); // Notify the subscribers that the user is not authenticated
  }

  private isAuthenticated(): boolean {
    const token = this.cookieService.get(ACCESS_TOKEN_KEY); // Get the access token from cookies
    return token !== null && !this.jwtHelper.isTokenExpired(token); // Check if the token is valid and not expired
  }
}
