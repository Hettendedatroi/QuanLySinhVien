import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Hanghoa } from '../models/hanghoa.model';

@Injectable({ providedIn: 'root' })
export class HanghoaService {
  private http = inject(HttpClient);
  private url = '/api/Hanghoa';

  getAll() { return this.http.get<Hanghoa[]>(this.url); }
  getById(id: string) { return this.http.get<Hanghoa>(`${this.url}/${encodeURIComponent(id)}`); }
  create(h: Hanghoa) { return this.http.post<Hanghoa>(this.url, h); }
  update(id: string, h: Hanghoa) { return this.http.put<void>(`${this.url}/${encodeURIComponent(id)}`, h); }
  delete(id: string) { return this.http.delete<{ message: string }>(`${this.url}/${encodeURIComponent(id)}`); }
}
