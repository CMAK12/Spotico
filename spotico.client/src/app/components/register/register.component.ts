import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CustomerService } from '../../services/customer.service';
import { UserDTO } from '../../DTOs/user.dto';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegisterComponent {
  private customerService = inject(CustomerService);
  private authService = inject(AuthService);
  private router = inject(Router);

  registerForm = new FormGroup({
    username: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl('')
  }, { validators: this.passwordMatchValidator });

  private passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password').value;
    const confirmPassword = formGroup.get('confirmPassword').value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit() {
    if (this.registerForm.invalid) return;

    const customer: UserDTO = {
      username: this.registerForm.get('username').value,
      email: this.registerForm.get('email').value,
      password: this.registerForm.get('password').value,
      role: null
    };

    // Call the customer service to register the user
    this.customerService.postCustomer(customer).subscribe(() => {
      this.authService.login({ email: customer.email, password: customer.password }).subscribe(() => {
        this.router.navigate(['/']);
      });
    });
  }
}
