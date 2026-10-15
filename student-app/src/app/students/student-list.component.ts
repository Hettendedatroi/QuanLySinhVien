import { Component, OnInit } from '@angular/core';
import { Student } from '../models/student';
import { StudentService } from '../services/student.service';

// Component CHA
@Component({
  selector: 'app-student-list',
  template: `
    <h2>Danh sách sinh viên</h2>

    <input class="form-control mb-3" placeholder="Tìm theo tên..." [(ngModel)]="keyword">

    <div *ngIf="selectedStudent" class="alert">
      Đã chọn: <strong>{{ selectedStudent.name }}</strong>
      <a [routerLink]="['/students', selectedStudent.id]" class="alert-link ml-2">Xem chi tiết</a>
    </div>

    <div class="table-wrap" *ngIf="filtered().length > 0">
      <table class="sv-table">
        <thead>
          <tr>
            <th>Mã SV</th>
            <th>Họ và tên</th>
            <th>Lớp</th>
            <th>GPA</th>
            <th>Trạng thái</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr app-student-row
              *ngFor="let s of filtered()"
              [student]="s"
              [selected]="selectedStudent?.id === s.id"
              [class.selected]="selectedStudent?.id === s.id"
              (picked)="onPicked($event)">
          </tr>
        </tbody>
      </table>
    </div>

    <p *ngIf="filtered().length === 0">Không tìm thấy sinh viên nào.</p>
  `
})
export class StudentListComponent implements OnInit {
  students: Student[] = [];
  keyword = '';
  selectedStudent: Student | undefined;

  constructor(private studentService: StudentService) {}

  ngOnInit() {
    this.students = this.studentService.getStudents();
  }

  filtered(): Student[] {
    const k = this.keyword.trim().toLowerCase();
    return this.students.filter(s => s.name.toLowerCase().indexOf(k) !== -1);
  }

  onPicked(student: Student) {
    this.selectedStudent = student;
  }
}