import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TodoTask } from '../models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = 'http://localhost:5000/api/tasks'; // Sprawdź port swojego API!

  constructor(private http: HttpClient) { }

  getTasks(): Observable<TodoTask[]> {
    return this.http.get<TodoTask[]>(this.apiUrl);
  }

  addTask(task: TodoTask): Observable<TodoTask> {
    return this.http.post<TodoTask>(this.apiUrl, task);
  }
}