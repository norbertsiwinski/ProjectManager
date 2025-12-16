export interface Task {
    id: string,
    name: string;
    status: string;
    assigneeName?: string | null;
    assigneeId?: string | null;
}