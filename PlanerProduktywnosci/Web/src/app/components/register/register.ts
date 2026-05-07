import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class RegisterComponent {
  newUser = {
    username: '',
    password: ''
  };

  constructor(
    private authService: AuthService, 
    private router: Router
  ) {}

  register() {
    if (!this.newUser.username || !this.newUser.password) {
      alert('Proszę wypełnić wszystkie pola!');
      return;
    }

    this.authService.register(this.newUser).subscribe({
      next: (response) => {
        console.log('Rejestracja udana:', response);
        alert('Konto utworzone pomyślnie! Możesz się teraz zalogować.');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error('Błąd rejestracji:', err);
        alert('Błąd rejestracji: ' + (err.error || 'Spróbuj ponownie później'));
      }
    });
  }
}