import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class LoginComponent {
  user = { username: '', password: '' };

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.user).subscribe({
      next: (token) => {
        this.authService.setToken(token);
        alert('Zalogowano pomyślnie!');
        this.router.navigate(['/']); // Przekierowanie do listy zadań
      },
      error: (err) => alert('Błąd logowania: ' + err.error)
    });
  }
}