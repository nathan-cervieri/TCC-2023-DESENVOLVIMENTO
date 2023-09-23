import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { CodeEditorModule } from '@ngstack/code-editor';
import { ReportComponent } from './report/report.component';

@NgModule({
  declarations: [AppComponent, ReportComponent],
  imports: [BrowserModule, CodeEditorModule.forRoot(), HttpClientModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
