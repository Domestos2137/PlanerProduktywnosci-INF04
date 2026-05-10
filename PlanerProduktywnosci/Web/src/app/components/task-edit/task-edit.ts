import { Component, OnInit, ChangeDetectorRef } from '@angular/core'; 
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { TodoTask } from '../../models/task.model'; 

@Component({
  selector: 'app-task-edit',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './task-edit.html' 
})
export class TaskEditComponent implements OnInit {
  task: TodoTask = {
    title: '',
    description: '',
    createdAt: new Date(),
    status: 'Nowe',
    priority: 'Normalny',
    category: 'Inne'
  };

  constructor(
    private route: ActivatedRoute,
    private taskService: TaskService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.taskService.getTaskById(id).subscribe({
        next: (data) => {
          this.task = data;
          if (this.task.dueDate) {
            this.task.dueDate = new Date(this.task.dueDate);
          }
          this.cdr.detectChanges();
        },
        error: (err) => console.error('Błąd ładowania zadania', err)
      });
    }
  }

  saveTask() {
    if (this.task.id) {
      this.taskService.updateTask(this.task.id, this.task).subscribe({
        next: () => {
          alert('Zadanie zostało zaktualizowane!');
          this.router.navigate(['/']);
        },
        error: (err) => alert('Błąd podczas zapisu: ' + err.message)
      });
    }
  }
}