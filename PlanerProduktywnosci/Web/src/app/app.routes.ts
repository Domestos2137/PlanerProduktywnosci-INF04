import { Routes } from '@angular/router';
import { TaskListComponent } from './components/task-list/task-list'; // Upewnij się, że ścieżka pasuje

export const routes: Routes = [
  { path: '', component: TaskListComponent }, // Strona główna
  // Tutaj w przyszłości dodasz np. { path: 'login', component: LoginComponent }
];