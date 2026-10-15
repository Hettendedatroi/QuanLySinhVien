import { Component, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { UiService } from './services/ui.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, AsyncPipe],
  template: `
    @if (ui.loading$ | async) {
      <div class="loading">Đang tải...</div>
    }

    @if (ui.toast$ | async; as t) {
      <div class="toast" [class.error]="t.type === 'error'">{{ t.text }}</div>
    }

    <main>
      <router-outlet />
    </main>
  `,
})
export class App {
  ui = inject(UiService);
}