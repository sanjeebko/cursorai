import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, RegisterRequest } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="register-container">
      <div class="register-card">
        <h2>Join Nepali Community</h2>
        <form (ngSubmit)="onSubmit()" #registerForm="ngForm">
          <div class="form-row">
            <div class="form-group">
              <label for="firstName">First Name</label>
              <input 
                type="text" 
                id="firstName" 
                name="firstName" 
                [(ngModel)]="registerData.firstName" 
                required 
                class="form-control"
                placeholder="Enter your first name">
            </div>
            
            <div class="form-group">
              <label for="lastName">Last Name</label>
              <input 
                type="text" 
                id="lastName" 
                name="lastName" 
                [(ngModel)]="registerData.lastName" 
                required 
                class="form-control"
                placeholder="Enter your last name">
            </div>
          </div>
          
          <div class="form-group">
            <label for="email">Email</label>
            <input 
              type="email" 
              id="email" 
              name="email" 
              [(ngModel)]="registerData.email" 
              required 
              email
              class="form-control"
              placeholder="Enter your email">
          </div>
          
          <div class="form-group">
            <label for="password">Password</label>
            <input 
              type="password" 
              id="password" 
              name="password" 
              [(ngModel)]="registerData.password" 
              required
              minlength="6"
              class="form-control"
              placeholder="Enter your password (min 6 characters)">
          </div>
          
          <div class="form-group">
            <label for="phoneNumber">Phone Number (Optional)</label>
            <input 
              type="tel" 
              id="phoneNumber" 
              name="phoneNumber" 
              [(ngModel)]="registerData.phoneNumber" 
              class="form-control"
              placeholder="Enter your phone number">
          </div>
          
          <div class="form-group">
            <label for="address">Address (Optional)</label>
            <input 
              type="text" 
              id="address" 
              name="address" 
              [(ngModel)]="registerData.address" 
              class="form-control"
              placeholder="Enter your address">
          </div>
          
          <div class="form-row">
            <div class="form-group">
              <label for="city">City (Optional)</label>
              <input 
                type="text" 
                id="city" 
                name="city" 
                [(ngModel)]="registerData.city" 
                class="form-control"
                placeholder="Enter your city">
            </div>
            
            <div class="form-group">
              <label for="country">Country (Optional)</label>
              <input 
                type="text" 
                id="country" 
                name="country" 
                [(ngModel)]="registerData.country" 
                class="form-control"
                placeholder="Enter your country">
            </div>
          </div>
          
          <button type="submit" class="btn btn-primary" [disabled]="!registerForm.valid || isLoading">
            {{ isLoading ? 'Creating account...' : 'Create Account' }}
          </button>
        </form>
        
        <div class="register-footer">
          <p>Already have an account? <a routerLink="/login">Login here</a></p>
        </div>
        
        <div *ngIf="error" class="error-message">
          {{ error }}
        </div>
      </div>
    </div>
  `,
  styles: [`
    .register-container {
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 100vh;
      background: linear-gradient(135deg, #007bff, #0056b3);
      padding: 2rem;
    }
    
    .register-card {
      background: white;
      padding: 2rem;
      border-radius: 1rem;
      box-shadow: 0 4px 6px rgba(0,0,0,0.1);
      width: 100%;
      max-width: 500px;
    }
    
    .register-card h2 {
      text-align: center;
      margin-bottom: 2rem;
      color: #2c3e50;
    }
    
    .form-row {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 1rem;
    }
    
    .form-group {
      margin-bottom: 1.5rem;
    }
    
    .form-group label {
      display: block;
      margin-bottom: 0.5rem;
      color: #2c3e50;
      font-weight: 500;
    }
    
    .form-control {
      width: 100%;
      padding: 0.75rem;
      border: 1px solid #dee2e6;
      border-radius: 0.5rem;
      font-size: 1rem;
      transition: border-color 0.3s ease;
    }
    
    .form-control:focus {
      outline: none;
      border-color: #007bff;
      box-shadow: 0 0 0 2px rgba(0,123,255,0.25);
    }
    
    .btn {
      width: 100%;
      padding: 0.75rem;
      border: none;
      border-radius: 0.5rem;
      font-size: 1rem;
      font-weight: 600;
      cursor: pointer;
      transition: all 0.3s ease;
    }
    
    .btn-primary {
      background-color: #007bff;
      color: white;
    }
    
    .btn-primary:hover:not(:disabled) {
      background-color: #0056b3;
      transform: translateY(-2px);
    }
    
    .btn-primary:disabled {
      background-color: #6c757d;
      cursor: not-allowed;
    }
    
    .register-footer {
      text-align: center;
      margin-top: 1.5rem;
      padding-top: 1.5rem;
      border-top: 1px solid #dee2e6;
    }
    
    .register-footer a {
      color: #007bff;
      text-decoration: none;
    }
    
    .register-footer a:hover {
      text-decoration: underline;
    }
    
    .error-message {
      background-color: #f8d7da;
      color: #721c24;
      padding: 0.75rem;
      border-radius: 0.5rem;
      margin-top: 1rem;
      text-align: center;
    }
    
    @media (max-width: 768px) {
      .form-row {
        grid-template-columns: 1fr;
      }
    }
  `]
})
export class RegisterComponent {
  registerData: RegisterRequest = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    phoneNumber: '',
    address: '',
    city: '',
    country: ''
  };
  
  isLoading = false;
  error = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit(): void {
    if (this.registerData.firstName && this.registerData.lastName && 
        this.registerData.email && this.registerData.password) {
      this.isLoading = true;
      this.error = '';

      this.authService.register(this.registerData).subscribe({
        next: () => {
          this.router.navigate(['/']);
        },
        error: (error) => {
          this.error = error.error?.message || 'Registration failed. Please try again.';
          this.isLoading = false;
        }
      });
    }
  }
} 