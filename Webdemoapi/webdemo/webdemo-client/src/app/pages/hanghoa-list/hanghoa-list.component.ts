import { Component, inject } from '@angular/core';
import { AsyncPipe, DecimalPipe } from '@angular/common';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import {
  BehaviorSubject, catchError, combineLatest, debounceTime,
  distinctUntilChanged, map, of, startWith, switchMap, tap,
} from 'rxjs';
import { Hanghoa } from '../../models/hanghoa.model';
import { HanghoaService } from '../../services/hanghoa.service';
import { AuthService } from '../../services/auth.service';
import { UiService } from '../../services/ui.service';

@Component({
  selector: 'app-hanghoa-list',
  imports: [AsyncPipe, DecimalPipe, ReactiveFormsModule, RouterLink],
  styleUrl: './hanghoa-list.component.css',
  template: `
    <header class="page-heading">
      <div>
        <p class="eyebrow">QUẢN LÝ KHO HÀNG</p>
        <h2>Danh sách hàng hóa</h2>
        @if (auth.user$ | async; as user) {
          <p class="welcome">Xin chào, {{ user.username }}</p>
        }
      </div>
      <button type="button" class="logout-button" (click)="logout()">Đăng xuất</button>
    </header>
    <div class="bar">
      <input [formControl]="search" placeholder="Tìm theo mã hoặc tên..." />
      <a routerLink="/hanghoa/new" class="btn">+ Thêm mới</a>
    </div>
    <table>
      <thead>
        <tr><th>Mã</th><th>Tên</th><th>Đơn giá</th><th>Số lượng</th><th></th></tr>
      </thead>
      <tbody>
        @for (h of items$ | async; track h.maHangHoa) {
          <tr>
            <td>{{ h.maHangHoa }}</td>
            <td>{{ h.tenHangHoa }}</td>
            <td>{{ h.donGia | number }}</td>
            <td>{{ h.soLuong }}</td>
            <td>
              <a [routerLink]="['/hanghoa', h.maHangHoa, 'edit']">Sửa</a>
              <button (click)="remove(h)">Xóa</button>
            </td>
          </tr>
        } @empty {
          <tr><td colspan="5">Không có dữ liệu</td></tr>
        }
      </tbody>
    </table>
  `,
})
export class HanghoaListComponent {
  private service = inject(HanghoaService);
  private ui = inject(UiService);
  readonly auth = inject(AuthService);
  private router = inject(Router);

  search = new FormControl('', { nonNullable: true });
  private reload$ = new BehaviorSubject<void>(undefined);

  items$ = combineLatest([
    this.reload$.pipe(
      switchMap(() => this.service.getAll().pipe(catchError(() => of([] as Hanghoa[])))),
      tap(list => console.log('Đã tải', list.length, 'hàng hóa')),
    ),
    this.search.valueChanges.pipe(debounceTime(300), distinctUntilChanged(), startWith('')),
  ]).pipe(
    map(([list, kw]) => {
      const k = kw.trim().toLowerCase();
      return list.filter(h => `${h.maHangHoa} ${h.tenHangHoa}`.toLowerCase().includes(k));
    }),
  );

  remove(h: Hanghoa) {
    if (!confirm(`Xóa "${h.tenHangHoa}"?`)) return;
    this.service.delete(h.maHangHoa).subscribe(res => {
      this.ui.toast(res.message);
      this.reload$.next();
    });
  }

  logout() {
    this.auth.logout().subscribe({
      next: () => this.router.navigate(['/login']),
      error: () => this.ui.toast('Không thể đăng xuất. Vui lòng thử lại.', 'error'),
    });
  }
}
