import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { TodoTask } from '../../models/task.model'; // Upewnij się, że to Twoja ścieżka do modelu!

@Component({
  selector: 'app-task-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './task-detail.html'
})
export class TaskDetailComponent implements OnInit {
  task?: TodoTask;
  isLoading: boolean = true; // Dodajemy flagę ładowania

  constructor(
    private route: ActivatedRoute,
    private taskService: TaskService,
    private router: Router
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    
    if (id) {
      this.taskService.getTaskById(id).subscribe({
        next: (data) => {
          console.log('Pobrano szczegóły:', data);
          this.task = data;
          this.isLoading = false;
        },
        error: (err) => {
          console.error('Błąd pobierania szczegółów:', err);
          this.isLoading = false;
        }
      });
    } else {
      this.isLoading = false;
    }
  }

  goBack() {
    this.router.navigate(['/']);
  }
}