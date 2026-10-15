import { Component, OnInit } from '@angular/core';
import { StudentService } from '../services/student.service';

@Component({
  selector: 'app-home',
  template: `
    <div class="jumbotron">
      <p class="lead">Hiện có <strong>{{ total }}</strong> sinh viên trong danh sách.</p>
      <a class="btn btn-primary" routerLink="/students">Xem danh sách sinh viên</a>
    </div>
  `
})
export class HomeComponent implements OnInit {
  userName = '';
  total = 0;

  constructor(private studentService: StudentService) {}

  ngOnInit() {
    this.total = this.studentService.getStudents().length;
  }
}
