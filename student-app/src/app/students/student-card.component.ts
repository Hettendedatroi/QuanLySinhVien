import { Component, Input, Output, EventEmitter, OnChanges, OnDestroy } from '@angular/core';
import { Student } from '../models/student';

// Component CON: là một dòng (<tr>) của bảng
@Component({
  selector: '[app-student-row]',
  template: `
    <td>{{ student.code }}</td>
    <td>
      <span class="avatar">{{ student.name | initials }}</span>
      {{ student.name }}
    </td>
    <td>{{ student.className }}</td>
    <td>{{ student.gpa | number:'1.1-2' }}</td>
    <td [ngSwitch]="student.status">
      <span *ngSwitchCase="'active'" class="badge badge-success">Đang học</span>
      <span *ngSwitchCase="'paused'" class="badge badge-warning">Bảo lưu</span>
      <span *ngSwitchDefault class="badge badge-secondary">Không rõ</span>
    </td>
    <td class="actions">
      <button class="btn btn-sm" (click)="onSelect()">Chọn</button>
      <a [routerLink]="['/students', student.id]">Chi tiết</a>
    </td>
  `
})
export class StudentCardComponent implements OnChanges, OnDestroy {
  @Input() student!: Student;
  @Input() selected = false;
  @Output() picked = new EventEmitter<Student>();

  ngOnChanges() {
    console.log('ngOnChanges:', this.student && this.student.name);
  }

  onSelect() {
    this.picked.emit(this.student);
  }

  ngOnDestroy() {
    console.log('ngOnDestroy:', this.student && this.student.name);
  }
}