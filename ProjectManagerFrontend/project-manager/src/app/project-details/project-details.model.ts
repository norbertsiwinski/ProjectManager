export interface ProjectDetails {
  id: string; 
  name: string;
  taskItems: TaskItem[];
}

export interface TaskItem {
  name: string;
  status: string;
  assigneeName?: string | null;
  assigneeId?: string | null;
}
