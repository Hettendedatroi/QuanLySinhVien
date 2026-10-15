import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
   <h1 class="text-center py-3 mb-0">Xin chào, Admin!</h1>
    <nav class="navbar navbar-expand navbar-dark" style="background-color: #0d6efd">
      <a class="navbar-brand" routerLink="/home">Quản lý SV</a>
      <ul class="navbar-nav">
        <li class="nav-item" routerLinkActive="active">
          <a class="nav-link" routerLink="/home">Trang chủ</a>
        </li>
        <li class="nav-item" routerLinkActive="active">
          <a class="nav-link" routerLink="/students">Danh sách</a>
        </li>
      </ul>
    </nav>

    <main class="container my-4">
      <router-outlet></router-outlet>
    </main>
  `
})
export class AppComponent {}
