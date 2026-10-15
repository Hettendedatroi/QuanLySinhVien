import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { map } from 'rxjs';
import { HanghoaService } from '../../services/hanghoa.service';
import { UiService } from '../../services/ui.service';

// Validator tự viết: số lượng phải là số nguyên
export const integerValidator: ValidatorFn = c =>
  Number.isInteger(c.value) ? null : { integer: true };

@Component({
  selector: 'app-hanghoa-form',
  imports: [ReactiveFormsModule, RouterLink],
  template: `
    <h2>{{ isEdit ? 'Sửa hàng hóa' : 'Thêm hàng hóa' }}</h2>
    <form [formGroup]="form" (ngSubmit)="submit()">

      <label>Mã hàng hóa
        <input formControlName="maHangHoa" />
      </label>
      @if (f.maHangHoa.touched && f.maHangHoa.errors; as e) {
        <small class="err">
          @if (e['required']) { Bắt buộc nhập mã. }
          @else if (e['pattern']) { Mã phải có dạng HH001. }
        </small>
      }

      <label>Tên hàng hóa
        <input formControlName="tenHangHoa" />
      </label>
      @if (f.tenHangHoa.touched && f.tenHangHoa.errors; as e) {
        <small class="err">
          @if (e['required']) { Bắt buộc nhập tên. }
          @else if (e['maxlength']) { Tối đa 100 ký tự. }
        </small>
      }

      <label>Đơn giá
        <input type="number" formControlName="donGia" />
      </label>
      @if (f.donGia.touched && f.donGia.errors; as e) {
        <small class="err">Đơn giá phải lớn hơn 0.</small>
      }

      <label>Số lượng
        <input type="number" formControlName="soLuong" />
      </label>
      @if (f.soLuong.touched && f.soLuong.errors; as e) {
        <small class="err">
          @if (e['min']) { Số lượng không được âm. }
          @else { Số lượng phải là số nguyên. }
        </small>
      }

      <div class="actions">
        <button type="submit">Lưu</button>
        <a routerLink="/hanghoa">Hủy</a>
      </div>
    </form>
  `,
})
export class HanghoaFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private service = inject(HanghoaService);
  private ui = inject(UiService);

  id = this.route.snapshot.paramMap.get('id');
  isEdit = !!this.id;

  form = this.fb.nonNullable.group({
    maHangHoa: ['', [Validators.required, Validators.pattern(/^HH\d{3}$/)]],
    tenHangHoa: ['', [Validators.required, Validators.maxLength(100)]],
    donGia: [0, [Validators.required, Validators.min(1)]],
    soLuong: [0, [Validators.required, Validators.min(0), integerValidator]],
  });

  get f() { return this.form.controls; }

  ngOnInit() {
    if (this.id) {
      this.form.controls.maHangHoa.disable();   // không cho sửa khóa chính
      this.service.getById(this.id).subscribe(h => this.form.patchValue(h));
    }
  }

  submit() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    const value = this.form.getRawValue();      // lấy cả trường đã disable

    const request$ = this.isEdit
      ? this.service.update(this.id!, value).pipe(map(() => 'Cập nhật thành công'))
      : this.service.create(value).pipe(map(() => 'Thêm mới thành công'));

    request$.subscribe(msg => {
      this.ui.toast(msg);
      this.router.navigate(['/hanghoa']);
    });
  }
}
