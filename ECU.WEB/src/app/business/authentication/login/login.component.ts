
import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  user: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router){

  }

  login(): void {

    console.log('User:', this.user);  // Agrega console.log para verificar el valor
    console.log('Password:', this.password);

    this.authService.login(this.user, this.password).subscribe({
      next: (response) => {
        const token = response.token;
        const payload = JSON.parse(atob(token.split('.')[1]));
        const role = payload.role;
        const mensaje = response.mensaje;
        const tieneError = response.tieneError;
        if (tieneError) {
          this.errorMessage = mensaje;
          return;
        }
        if (role === 'admin') {
          this.router.navigate(['/dashboard'])
        } else {
          this.router.navigate(['/profile'])
        }
      },
      error: (err) => { this.errorMessage = "Error"; console.error('Login failed', err) }
    })
  }

}
