import { Routes } from '@angular/router';
import { TaskListComponent } from './components/task-list/task-list'; // Upewnij się, że ścieżka pasuje
import { LoginComponent } from './components/login/login';
import { authGuard } from './guards/auth-guard';
import { RegisterComponent } from './components/register/register';
export const routes: Routes = [
  { path: '', component: TaskListComponent, canActivate: [authGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent }
];