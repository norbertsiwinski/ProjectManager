export interface ProjectDetails {
  id: string;
  name: string;
  taskItems: TaskItem[];
  projectMembers: ProjectMember[];
}

export interface TaskItem {
  id: string,
  name: string;
  status: string;
  assigneeName?: string | null;
}

export interface ProjectMember {
  id: string;
  userEmail: string;
  userRole: string;
}
