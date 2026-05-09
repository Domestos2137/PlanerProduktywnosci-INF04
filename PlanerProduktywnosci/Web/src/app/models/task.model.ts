export interface TodoTask {
  id?: number;
  title: string;
  description: string;
  createdAt?: Date;
  dueDate?: Date;
  status: string;
  priority: string;
  category: string;
}