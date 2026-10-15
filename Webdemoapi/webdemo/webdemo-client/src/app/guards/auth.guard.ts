import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { catchError, map, of } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (_route, state) => {
  const router = inject(Router);
  const auth = inject(AuthService);

  return auth.me().pipe(
    map(() => true),
    catchError(() => {
      auth.clearCurrentUser();
      return of(router.createUrlTree(['/login'], {
        queryParams: { returnUrl: state.url },
      }));
    }),
  );
};
