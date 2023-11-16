import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { CodeEditorModule } from '@ngstack/code-editor';
import { CodeVisualizerComponent } from './code-visualizer/code-visualizer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SingleCodeConverterComponent } from './single-code-converter/single-code-converter.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { LanguageLoader } from './language-loader/language-loader';
import { ReportListComponent } from './report-list/report-list.component';
import { VersionPickerComponent } from './version-picker/version-picker.component';
import { FolderInputComponent } from './folder-input/folder-input.component';
import { FileConverterComponent } from './file-converter/file-converter.component';
import { FileConverterListComponent } from './file-converter-list/file-converter-list.component';

function LanguageLoaderConstructor() {
  return new LanguageLoader();
}

@NgModule({
  declarations: [
    AppComponent,
    CodeVisualizerComponent,
    SingleCodeConverterComponent,
    ReportListComponent,
    VersionPickerComponent,
    FolderInputComponent,
    FileConverterComponent,
    FileConverterListComponent,
  ],
  imports: [
    BrowserModule,
    CodeEditorModule.forRoot(),
    HttpClientModule,
    BrowserAnimationsModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: LanguageLoaderConstructor,
      },

      defaultLanguage: 'pt',
    }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
