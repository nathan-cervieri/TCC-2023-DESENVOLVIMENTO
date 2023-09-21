import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { CodeEditorModule } from '@ngstack/code-editor';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, CodeEditorModule.forRoot(), HttpClientModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
