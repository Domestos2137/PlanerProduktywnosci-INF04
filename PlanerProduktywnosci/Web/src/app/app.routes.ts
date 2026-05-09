import { Routes } from '@angular/router';
import { TaskListComponent } from './components/task-list/task-list'; 
import { LoginComponent } from './components/login/login';
import { authGuard } from './guards/auth-guard';
import { RegisterComponent } from './components/register/register';
import { TaskDetailComponent } from './components/task-detail/task-detail'; 
import { TaskEditComponent } from './components/task-edit/task-edit';    

export const routes: Routes = [
  { path: '', component: TaskListComponent, canActivate: [authGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'tasks/:id', component: TaskDetailComponent, canActivate: [authGuard] },
  { path: 'tasks/edit/:id', component: TaskEditComponent, canActivate: [authGuard] }
];