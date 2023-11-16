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

  versionFrom = 14;
  versionTo = 15;

  fileList: File[] = [];

  constructor(public translate: TranslateService) {
    translate.addLangs(['en', 'pt']);
    translate.setDefaultLang('pt');

    translate.use('pt');
  }
}
