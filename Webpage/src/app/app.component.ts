import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CodeVisualizer } from './model/report';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {

  convertedCodeVisualizer?: CodeVisualizer;

  versionFrom = 15;
  versionTo = 16;

  fileList: File[] = [];

  constructor(public translate: TranslateService) {
    translate.addLangs(['en', 'pt']);
    translate.setDefaultLang('pt');

    translate.use('pt');
  }
}
