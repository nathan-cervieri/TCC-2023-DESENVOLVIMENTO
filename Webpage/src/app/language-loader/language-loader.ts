import { TranslateLoader } from '@ngx-translate/core';
import { Observable, of } from 'rxjs';
import pt from '../../assets/i18n/pt.json';
import en from '../../assets/i18n/en.json';

export class LanguageLoader implements TranslateLoader {
  getTranslation(lang: string): Observable<Object> {
    switch (lang) {
      case 'en':
        return of(en);
      case 'pt':
      default:
        return of(pt);
    }
  }
}
