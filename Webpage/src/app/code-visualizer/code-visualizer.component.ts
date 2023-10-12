import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { CodeEditorComponent, CodeModel } from '@ngstack/code-editor';

const BASE_CODE_MODEL = {
  language: 'typescript',
  uri: 'main.ts',
  value: '',
  readonly: false,
};
@Component({
  selector: 'app-code-visualizer',
  templateUrl: './code-visualizer.component.html',
  styleUrls: ['./code-visualizer.component.scss'],
})
export class CodeVisualizerComponent implements OnChanges {
  @Input()
  public code: string = '';
  @Input()
  public readonly = false;
  @Input()
  public uri = 'main.ts';

  @Output()
  private codeChanged: EventEmitter<string> = new EventEmitter();

  @ViewChild('codeEditor')
  codeEditor?: CodeEditorComponent;

  theme = 'vs-dark';

  codeModel: CodeModel;

  options = {
    contextmenu: true,
    minimap: {
      enabled: true,
    },
  };

  ngOnChanges(changes: SimpleChanges): void {
    const baseModel = { ...BASE_CODE_MODEL };
    baseModel.value = this.code;
    baseModel.readonly = this.readonly;
    baseModel.uri = this.uri;
    this.codeModel = baseModel;
  }

  constructor() {
    this.codeModel = BASE_CODE_MODEL;
  }

  onCodeChanged(value: string) {
    this.codeChanged.emit(value);
  }
}
