import { Component, inject } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { LoginDTO } from '../../DTOs/login.dto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  authService = inject(AuthService);
  router = inject(Router);

  // Create a FormGroup with email and password fields 
  loginForm = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [Validators.required])
  });

  onSubmit() {
    // Map the form values to a LoginDTO object
    const request: LoginDTO = {
      email: this.loginForm.get('email').value,
      password: this.loginForm.get('password').value
    };
    
    if (this.loginForm.valid) {
      this.authService.login(request).subscribe({
        next: () => {
          // If login is successful, navigate to the home page
          console.log('Login successful');
          this.router.navigate(['/']);
        },
        error: (err) => {
          // If login fails, log the error to the console
          console.error('Login failed:', err);
        }
      });
    }
  }
}
