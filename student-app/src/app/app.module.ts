import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { StudentListComponent } from './students/student-list.component';
import { StudentCardComponent } from './students/student-card.component';
import { StudentDetailComponent } from './students/student-detail.component';
import { InitialsPipe } from './pipes/initials.pipe';
import { StudentService } from './services/student.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    StudentListComponent,
    StudentCardComponent,
    StudentDetailComponent,
    InitialsPipe
  ],
  imports: [BrowserModule, FormsModule, AppRoutingModule],
  providers: [StudentService],
  bootstrap: [AppComponent]
})
export class AppModule {}
