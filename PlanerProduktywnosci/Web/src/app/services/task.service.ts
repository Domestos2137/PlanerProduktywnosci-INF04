import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TodoTask } from '../models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = 'http://localhost:5290/api/tasks'; 

  constructor(private http: HttpClient) { }

  getTasks(): Observable<TodoTask[]> {
    return this.http.get<TodoTask[]>(this.apiUrl);
  }

  getTaskById(id: number): Observable<TodoTask> {
    return this.http.get<TodoTask>(`${this.apiUrl}/${id}`);
  }

  addTask(task: TodoTask): Observable<TodoTask> {
    return this.http.post<TodoTask>(this.apiUrl, task);
  }

  deleteTask(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  updateTask(id: number, task: TodoTask): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, task);
  }
}