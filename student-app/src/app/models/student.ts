export interface Student {
  id: number;
  code: string;
  name: string;
  className: string;
  email: string;
  gpa: number;
  status: 'active' | 'paused';
}
