import { Component } from '@angular/core';
import { CodeModel } from '@ngstack/code-editor/public_api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  theme = 'vs-dark';

  codeModel: CodeModel = {
    language: 'typescript',
    uri: 'main.ts',
    value: '',
  };

  options = {
    contextmenu: true,
    minimap: {
      enabled: true,
    },
  };

  onCodeChanged(value: string) {
    console.log(value);
  }

  getAllChangesFromVersions() {}
}
