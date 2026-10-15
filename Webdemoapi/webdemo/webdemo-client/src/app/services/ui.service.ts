import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface Toast { text: string; type: 'success' | 'error'; }

@Injectable({ providedIn: 'root' })
export class UiService {
  private pending = 0;
  readonly loading$ = new BehaviorSubject<boolean>(false);
  readonly toast$ = new BehaviorSubject<Toast | null>(null);

  show() { this.pending++; this.loading$.next(true); }
  hide() {
    this.pending = Math.max(0, this.pending - 1);
    if (this.pending === 0) this.loading$.next(false);
  }
  toast(text: string, type: Toast['type'] = 'success') {
    this.toast$.next({ text, type });
    setTimeout(() => this.toast$.next(null), 3000);
  }
}
