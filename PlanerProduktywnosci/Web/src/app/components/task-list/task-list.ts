import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { TaskService } from '../../services/task.service';
import { TodoTask } from '../../models/task.model';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-task-list',
  standalone: true, 
  imports: [CommonModule, FormsModule, RouterLink], 
  templateUrl: './task-list.html',
  styleUrl: './task-list.scss'
})
export class TaskListComponent implements OnInit {
  tasks: TodoTask[] = [];
  isLoading: boolean = true;
  searchText: string = '';
  filterStatus: string = '';
  filterCategory: string = '';
  filterDate: string = '';

  newTask: TodoTask = {
      title: '',
      description: '',
      status: 'Nowe',
      priority: 'Normalny',
      category: 'Praca',
      createdAt: new Date(),
      dueDate: undefined
  };

  constructor(
      private taskService: TaskService, 
      private cdr: ChangeDetectorRef,
      private router: Router
    ) {}

  ngOnInit(): void {
    this.loadTasks()
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

  loadTasks(): void {
      this.isLoading = true;
      this.taskService.getTasks().subscribe({
        next: (data) => {
          console.log('Dane w konsoli:', data);
          this.tasks = data;
          
          this.cdr.detectChanges(); 
          
          this.isLoading = false;
        },
        error: (err) => {
          console.error(err);
          this.isLoading = false;
          this.cdr.detectChanges(); 
        }
      });
    }

  private resetForm(): void {
    this.newTask = { title: '', description: '', status: 'Nowe', priority: 'Normalny', category: 'Praca', createdAt: new Date(), dueDate: undefined };
  }

  get filteredTasks() {
    return this.tasks.filter(task => {
      const matchesSearch = task.title.toLowerCase().includes(this.searchText.toLowerCase()) ||
                            (task.description && task.description.toLowerCase().includes(this.searchText.toLowerCase()));

      const matchesStatus = this.filterStatus ? task.status === this.filterStatus : true;

      const matchesCategory = this.filterCategory ? task.category === this.filterCategory : true;

      let matchesDate = true;
      if (this.filterDate && task.dueDate) {
        const taskDate = new Date(task.dueDate).toISOString().split('T')[0];
        matchesDate = taskDate === this.filterDate;
      } else if (this.filterDate && !task.dueDate) {
        matchesDate = false; 
      }

      return matchesSearch && matchesStatus && matchesCategory && matchesDate;
    });
  }
  clearFilters(): void {
    this.filterStatus = '';
    this.filterCategory = '';
    this.filterDate = '';
    this.searchText = '';
    this.cdr.detectChanges();
  }

  goToDetails(id: number | undefined): void {
    if (id) {
      this.router.navigate(['/tasks', id]);
    } else {
      alert('Błąd: Zadanie nie ma ID!');
    }
  }

  goToEdit(id: number | undefined): void {
    if (id) {
      this.router.navigate(['/tasks/edit', id]);
    } else {
      alert('Błąd: Zadanie nie ma ID!');
    }
  }
}
