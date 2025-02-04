import {
  ChangeDetectionStrategy,
  Component,
  DestroyRef,
  inject,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CustomerService } from '../../services/customer.service';
import { UserDTO } from '../../DTOs/user.dto';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { LoginDTO } from '../../DTOs/login.dto';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterComponent {
  private readonly customerService = inject(CustomerService);
  private readonly authService = inject(AuthService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly router = inject(Router);

  registerForm = new FormGroup(
    {
      username: new FormControl(''),
      email: new FormControl(''),
      password: new FormControl(''),
      confirmPassword: new FormControl(''),
    },
    { validators: this.passwordMatchValidator },
  );

  private passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password').value;
    const confirmPassword = formGroup.get('confirmPassword').value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit() {
    if (this.registerForm.invalid) return;

    // Create a User DTO object for post request
    const customer: UserDTO = {
      username: this.registerForm.get('username').value,
      email: this.registerForm.get('email').value,
      password: this.registerForm.get('password').value,
      role: null,
    };

    // Create a Login DTO object for log in request
    const loginDetails: LoginDTO = {
      email: customer.email,
      password: customer.password,
    };

    // Call the customer service to register the user
    this.customerService
      .postCustomer(customer)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.authService.login(loginDetails).subscribe(() => {
          this.router.navigate(['/']);
        });
      });
  }
}
