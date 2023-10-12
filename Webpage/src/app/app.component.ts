import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {

  versionFrom = 14;
  versionTo = 15;

  constructor(public translate: TranslateService) {
    translate.addLangs(['en', 'pt']);
    translate.setDefaultLang('pt');

    translate.use('pt');
  }
}
