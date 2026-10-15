import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, finalize, throwError } from 'rxjs';
import { UiService } from '../services/ui.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const ui = inject(UiService);
  ui.show();
  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      const msg =
        err.status === 0 ? 'Không kết nối được tới server (kiểm tra API/CORS).'
          : typeof err.error === 'string' && err.error ? err.error
            : err.error?.message ?? err.error?.title ?? `Lỗi ${err.status}`;
      if (!(err.status === 401 && req.url.endsWith('/api/auth/me'))) {
        ui.toast(msg, 'error');
      }
      return throwError(() => err);
    }),
    finalize(() => ui.hide())
  );
};
