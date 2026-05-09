import { Component, OnInit, ChangeDetectorRef } from '@angular/core'; // 1. DODANO ChangeDetectorRef
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { TodoTask } from '../../models/task.model';

@Component({
  selector: 'app-task-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './task-detail.html'
})
export class TaskDetailComponent implements OnInit {
  task?: TodoTask;
  isLoading: boolean = true;

  constructor(
    private route: ActivatedRoute,
    private taskService: TaskService,
    private router: Router,
    private cdr: ChangeDetectorRef // 2. DODANO DO KONSTRUKTORA
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    
    if (id) {
      this.taskService.getTaskById(id).subscribe({
        next: (data) => {
          this.task = data;
          this.isLoading = false;
          this.cdr.detectChanges(); // 3. SZTURCHAMY WIDOK!
        },
        error: (err) => {
          console.error('Błąd:', err);
          this.isLoading = false;
          this.cdr.detectChanges(); // 3. SZTURCHAMY WIDOK!
        }
      });
    } else {
      this.isLoading = false;
      this.cdr.detectChanges();
    }
  }

  goBack() {
    this.router.navigate(['/']);
  }
}