import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Student } from '../models/student';
import { StudentService } from '../services/student.service';

@Component({
  selector: 'app-student-detail',
  template: `
    <div *ngIf="student; else notFound" class="card">
      <div class="card-header"><h3 class="mb-0">{{ student.name }}</h3></div>
      <ul class="list-group list-group-flush">
        <li class="list-group-item"><strong>Mã SV:</strong> {{ student.code }}</li>
        <li class="list-group-item"><strong>Lớp:</strong> {{ student.className }}</li>
        <li class="list-group-item"><strong>Email:</strong> {{ student.email }}</li>
        <li class="list-group-item"><strong>GPA:</strong> {{ student.gpa }}</li>
      </ul>
      <div class="card-body">
        <a routerLink="/students" class="btn btn-secondary">&laquo; Quay lại danh sách</a>
      </div>
    </div>

    <ng-template #notFound>
      <div class="alert alert-warning">
        Không tìm thấy sinh viên.
        <a routerLink="/students" class="alert-link">Quay lại danh sách</a>
      </div>
    </ng-template>
  `
})
export class StudentDetailComponent implements OnInit {
     student: Student | undefined;

  constructor(private route: ActivatedRoute, private studentService: StudentService) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.student = this.studentService.getStudent(id);
  }
}
