import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FileConverterStatus } from './model/file-converter-status';
import { VersionConverterService } from '../service/version-converter.service';
import { CodeVisualizer, Report } from '../model/report';

@Component({
  selector: 'app-file-converter',
  templateUrl: './file-converter.component.html',
  styleUrls: ['./file-converter.component.scss']
})
export class FileConverterComponent implements OnInit {

  @Input()
  file: File | undefined = undefined;
  @Input()
  versionFrom = 15;
  @Input()
  versionTo = 16;

  @Input()
  shouldFilterAutomatic = false;
  @Input()
  shouldFilterManual = false;

  @Output()
  displayConvertedCode = new EventEmitter<CodeVisualizer>();

  fileContent = '';
  codeChangeReport?: Report;
  status = FileConverterStatus.IN_PROGRESS;

  constructor(private versionConverterService: VersionConverterService) {}

  ngOnInit(): void {
    this.convertFile();
  }

  get shouldDisplay(): boolean {
    if (!this.codeChangeReport) {
      return true;
    }

    if (this.shouldFilterAutomatic && this.shouldFilterManual && (this.codeChangeReport.hasAutomaticChange || this.codeChangeReport.hasManualChange)) {
      return true;
    }

    if (this.shouldFilterAutomatic && !this.codeChangeReport.hasAutomaticChange) {
      return false;
    }

    if (this.shouldFilterManual && !this.codeChangeReport.hasManualChange) {
      return false;
    }

    return true;
  }

  convertFile() {
    this.status = FileConverterStatus.IN_PROGRESS;

    const reader = new FileReader();
    reader.onload = (event) => {
      this.fileContent = event.target?.result as string;
      this.convertCode();
    }
    reader.onerror = (event) => {
      this.fileContent = event.target?.error?.message ?? '';
      this.status = FileConverterStatus.ERROR;
    }
    reader.readAsText(this.file!);
  }

  convertCode(): void {
    this.versionConverterService
      .getFileChangesFromVersions(this.fileContent ?? '', this.versionFrom, this.versionTo)
      .subscribe({
        next: (value) => {
          this.handleReportReturn(value);
          this.status = FileConverterStatus.SUCCESS;
        },
        error: (error) => {
          console.log(error);
          this.status = FileConverterStatus.ERROR;
        },
      });
  }

  emitCodeToShow() {
    const codeDisplay: CodeVisualizer = {
      baseFile: this.fileContent,
      returnReport: this.codeChangeReport
    };
    this.displayConvertedCode.emit(codeDisplay);
  }

  private handleReportReturn(report: Report) {
    this.codeChangeReport = report;
  }

}
