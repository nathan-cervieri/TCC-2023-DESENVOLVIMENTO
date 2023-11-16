import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CodeVisualizer } from '../model/report';

@Component({
  selector: 'app-file-converter-list',
  templateUrl: './file-converter-list.component.html',
  styleUrls: ['./file-converter-list.component.scss']
})
export class FileConverterListComponent {

  @Input()
  fileList: File[] = [];
  @Input()
  versionFrom = 14;
  @Input()
  versionTo = 15;

  @Output()
  displayConvertedCode = new EventEmitter<CodeVisualizer>();

  shouldDisplayFileConverterList() {
    return this.fileList?.length > 0
  }

  handleDisplayConvertedCode(convertedCode: CodeVisualizer) {
    this.displayConvertedCode.emit(convertedCode);
  }
}
