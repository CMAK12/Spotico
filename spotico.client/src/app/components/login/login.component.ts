import { Component, inject } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { LoginDTO } from '../../DTOs/login.dto';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  authService = inject(AuthService);

  loginForm = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [Validators.required])
  });

  onSubmit() {
    const request: LoginDTO = {
      email: this.loginForm.get('email').value,
      password: this.loginForm.get('password').value
    };
    
    if (this.loginForm.valid) {
      this.authService.login(request).subscribe({
        next: () => {
          console.log('Login successful');
        },
        error: (err) => {
          console.error('Login failed:', err);
        }
      });
    }
  }
}
