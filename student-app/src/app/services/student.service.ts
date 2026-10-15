import { Injectable } from '@angular/core';
import { Student } from '../models/student';

// Service: nơi chứa dữ liệu tĩnh, được inject vào các component (Dependency Injection)
@Injectable()
export class StudentService {
  private students: Student[] = [
    { id: 1, code: 'SV001', name: 'Nguyễn Văn An',   className: 'CNTT1', email: 'an.nv@example.com',    gpa: 3.4, status: 'active' },
    { id: 2, code: 'SV002', name: 'Trần Thị Bình',   className: 'CNTT2', email: 'binh.tt@example.com',  gpa: 3.7, status: 'active' },
    { id: 3, code: 'SV003', name: 'Lê Hoàng Cường',  className: 'CNTT1', email: 'cuong.lh@example.com', gpa: 2.9, status: 'paused' },
    { id: 4, code: 'SV004', name: 'Phạm Minh Dũng',  className: 'CNTT3', email: 'dung.pm@example.com',  gpa: 3.1, status: 'active' },
    { id: 5, code: 'SV005', name: 'Vũ Thu Hà',       className: 'CNTT2', email: 'ha.vt@example.com',    gpa: 3.9, status: 'active' }
  ];

  getStudents(): Student[] {
    return this.students;
  }

     getStudent(id: number): Student | undefined {
     return this.students.find(s => s.id === id);
   }
}
