import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';

export interface AuthUser {
  username: string;
}

interface AuthResponse extends AuthUser {
  message: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly currentUser = new BehaviorSubject<AuthUser | null>(null);
  readonly user$ = this.currentUser.asObservable();

  login(username: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(
      '/api/auth/login',
      { username, password },
      { withCredentials: true },
    ).pipe(tap(user => this.currentUser.next(user)));
  }

  register(username: string, password: string): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(
      '/api/auth/register',
      { username, password },
      { withCredentials: true },
    );
  }

  me(): Observable<AuthUser> {
    return this.http.get<AuthUser>(
      '/api/auth/me',
      { withCredentials: true },
    ).pipe(tap(user => this.currentUser.next(user)));
  }

  logout(): Observable<void> {
    return this.http.post<void>(
      '/api/auth/logout',
      {},
      { withCredentials: true },
    ).pipe(tap(() => this.currentUser.next(null)));
  }

  clearCurrentUser(): void {
    this.currentUser.next(null);
  }
}
