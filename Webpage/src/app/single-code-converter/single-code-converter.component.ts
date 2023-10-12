import { Component, Input } from '@angular/core';
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
  codeChangeReport?: Report;

  @Input() versionFrom = 14;
  @Input() versionTo = 15;

  constructor(private versionConverterService: VersionConverterService) {}

  convertCode(): void {
    this.versionConverterService
      .getFileChangesFromVersions(this.currentCode ?? '', this.versionFrom, this.versionTo)
      .subscribe({
        next: (value) => this.handleReportReturn(value),
        error: (error) => console.log(error),
      });
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
