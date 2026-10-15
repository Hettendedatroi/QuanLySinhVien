import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';
import { LoginComponent } from './pages/login/login.component';
import { HanghoaListComponent } from './pages/hanghoa-list/hanghoa-list.component';
import { HanghoaFormComponent } from './pages/hanghoa-form/hanghoa-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'hanghoa', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'hanghoa', component: HanghoaListComponent, canActivate: [authGuard] },
  { path: 'hanghoa/new', component: HanghoaFormComponent, canActivate: [authGuard] },
  { path: 'hanghoa/:id/edit', component: HanghoaFormComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: 'hanghoa' },
];
