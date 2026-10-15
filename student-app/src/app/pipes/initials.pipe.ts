import { Pipe, PipeTransform } from '@angular/core';

// Pipe tự viết: "Nguyễn Văn An" -> "NVA"
@Pipe({ name: 'initials' })
export class InitialsPipe implements PipeTransform {
  transform(value: string): string {
    if (!value) { return ''; }
    return value.split(' ').map(w => w.charAt(0)).join('').toUpperCase();
  }
}
