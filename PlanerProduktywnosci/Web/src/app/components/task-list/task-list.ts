import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { TaskService } from '../../services/task.service';
import { TodoTask } from '../../models/task.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-task-list',
  standalone: true, 
  imports: [CommonModule, FormsModule], 
  templateUrl: './task-list.html',
  styleUrl: './task-list.scss'
})
export class TaskListComponent implements OnInit {
  tasks: TodoTask[] = [];

  newTask: TodoTask = {
      title: '',
      description: '',
      status: 'Nowe',
      priority: 'Normalny',
      category: 'Praca',
      createdAt: new Date()
    };

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.taskService.getTasks().subscribe({
      next: (data) => {
        this.tasks = data;
        console.log('Pobrano zadania:', data);
      },
      error: (err) => {
        console.error('Błąd pobierania zadań:', err);
      }
    });
  }

saveTask(): void {
  if (this.newTask.title.trim()) {
    this.taskService.addTask(this.newTask).subscribe({
      next: (savedTask) => {
        this.loadTasks(); 
        this.resetForm();
        console.log('Zadanie dodane!');
      },
      error: (err) => console.error('Błąd dodawania:', err)
    });
  }
}

  deleteTask(id: number | undefined): void {
    if (id && confirm('Czy na pewno chcesz usunąć to zadanie?')) {
      this.taskService.deleteTask(id).subscribe(() => {
        this.loadTasks();
      });
    }
  }

  private loadTasks(): void {
    this.taskService.getTasks().subscribe(data => this.tasks = data);
  }

  private resetForm(): void {
    this.newTask = { title: '', description: '', status: 'Nowe', priority: 'Normalny', category: 'Praca', createdAt: new Date() };
  }
}