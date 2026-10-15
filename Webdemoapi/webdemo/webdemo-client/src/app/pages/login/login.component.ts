import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UiService } from '../../services/ui.service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  styleUrl: './login.component.css',
  template: `
    <section class="auth-card">
      <p class="eyebrow">QUẢN LÝ KHO HÀNG</p>
      <h1>{{ isRegistering ? 'Tạo tài khoản' : 'Chào mừng trở lại' }}</h1>
      <p class="subtitle">
        {{ isRegistering ? 'Đăng ký tài khoản để bắt đầu quản lý hàng hóa.' : 'Đăng nhập để xem danh sách hàng hóa.' }}
      </p>

      <form [formGroup]="form" (ngSubmit)="submit()">
        <label for="username">Tên đăng nhập</label>
        <input id="username" formControlName="username" autocomplete="username"
          placeholder="Nhập tên đăng nhập" minlength="3" maxlength="50" />
        @if (form.controls.username.touched && form.controls.username.invalid) {
          <small class="field-error">Tên đăng nhập cần có ít nhất 3 ký tự.</small>
        }

        <label for="password">Mật khẩu</label>
        <input id="password" type="password" formControlName="password"
          [autocomplete]="isRegistering ? 'new-password' : 'current-password'"
          placeholder="Ít nhất 8 ký tự" minlength="8" maxlength="128" />
        @if (form.controls.password.touched && form.controls.password.invalid) {
          <small class="field-error">Mật khẩu cần có ít nhất 8 ký tự.</small>
        }

        <button class="primary-button" type="submit" [disabled]="form.invalid || submitting">
          {{ submitting ? 'Đang xử lý...' : (isRegistering ? 'Tạo tài khoản' : 'Đăng nhập') }}
        </button>
      </form>

      <p class="switch-mode">
        {{ isRegistering ? 'Đã có tài khoản?' : 'Chưa có tài khoản?' }}
        <button type="button" (click)="toggleMode()">
          {{ isRegistering ? 'Đăng nhập' : 'Đăng ký' }}
        </button>
      </p>
    </section>
  `,
})
export class LoginComponent {
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private auth = inject(AuthService);
  private ui = inject(UiService);
  private fb = inject(FormBuilder);

  isRegistering = false;
  submitting = false;
  form = this.fb.nonNullable.group({
    username: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50), Validators.pattern(/^[a-zA-Z0-9_.-]+$/)]],
    password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(128)]],
  });

  toggleMode() {
    this.isRegistering = !this.isRegistering;
    this.form.reset();
  }

  submit() {
    if (this.form.invalid || this.submitting) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting = true;
    const { username, password } = this.form.getRawValue();
    const request$ = this.isRegistering
      ? this.auth.register(username, password)
      : this.auth.login(username, password);

    request$.subscribe({
      next: () => {
        this.submitting = false;
        if (this.isRegistering) {
          this.isRegistering = false;
          this.form.controls.password.reset();
          this.ui.toast('Đăng ký thành công. Bạn có thể đăng nhập.');
          return;
        }

        const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
        const destination = returnUrl?.startsWith('/') && !returnUrl.startsWith('//')
          ? returnUrl
          : '/hanghoa';
        this.router.navigateByUrl(destination);
      },
      error: () => {
        this.submitting = false;
      },
    });
  }
}
