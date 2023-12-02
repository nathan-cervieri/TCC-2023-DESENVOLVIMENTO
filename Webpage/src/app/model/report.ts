export class Report {
  public returnFile = '';
  public versionFrom = '';
  public versionTo = '';
  public hasManualChange = false;
  public HasAutomaticChange = false;
  public changes: ReportChange[] = [];
}

export class ReportChange {
  public originversion = 0;
  public changeDescription = '';
  public changeUrl = '';
  public linesChanged: number[] = [];
}

export class CodeVisualizer {
  public baseFile = '';
  public returnReport?: Report  = undefined;
}
