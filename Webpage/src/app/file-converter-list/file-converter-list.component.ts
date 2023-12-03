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

  shouldFilterAutomatic = false;
  shouldFilterManual = false;

  shouldDisplayFileConverterList() {
    return this.fileList?.length > 0
  }

  handleDisplayConvertedCode(convertedCode: CodeVisualizer) {
    this.displayConvertedCode.emit(convertedCode);
  }

  filterAutomatic() {
    this.shouldFilterAutomatic = !this.shouldFilterAutomatic;
  }

  filterManual() {
    this.shouldFilterManual = !this.shouldFilterManual;
  }

  clearFilter() {
    this.shouldFilterAutomatic = false;
    this.shouldFilterManual = false;
  }
}
