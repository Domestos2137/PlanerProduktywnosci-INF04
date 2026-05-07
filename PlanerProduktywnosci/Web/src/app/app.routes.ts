import { Routes } from '@angular/router';
import { TaskListComponent } from './components/task-list/task-list'; // Upewnij się, że ścieżka pasuje
import { LoginComponent } from './components/login/login';

export const routes: Routes = [
  { path: '', component: TaskListComponent }, // Strona główna
  { path: 'login', component: LoginComponent },
  // Tutaj w przyszłości dodasz np. { path: 'login', component: LoginComponent }
];