import { Component, Input, SimpleChanges } from '@angular/core';
import { VersionConverterService } from '../service/version-converter.service';
import { Report } from '../model/report';

@Component({
  selector: 'app-single-code-converter',
  templateUrl: './single-code-converter.component.html',
  styleUrls: ['./single-code-converter.component.scss'],
})
export class SingleCodeConverterComponent {
  currentCode?: string;
  fixedChangesReport?: Report;

  @Input()
  codeChangeReport?: Report;
  @Input()
  displayCode?: string;

  @Input() versionFrom = 15;
  @Input() versionTo = 16;

  constructor(private versionConverterService: VersionConverterService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (!this.codeChangeReport) {
      return;
    }

    this.loadStaticChanges();
  }

  convertCode(): void {
    this.loadStaticChanges();

    this.versionConverterService
      .getFileChangesFromVersions(this.currentCode ?? '', this.versionFrom, this.versionTo)
      .subscribe({
        next: (value) => this.handleReportReturn(value),
        error: (error) => console.log(error),
      });
  }

  loadStaticChanges(): void {
    this.versionConverterService
      .getAllStaticChangesFromVersions(this.versionFrom, this.versionTo)
      .subscribe({
        next: (value) => (this.fixedChangesReport = value),
        error: (error) => console.log(error),
      });
  }

  private handleReportReturn(report: Report) {
    this.codeChangeReport = report;
  }
}
