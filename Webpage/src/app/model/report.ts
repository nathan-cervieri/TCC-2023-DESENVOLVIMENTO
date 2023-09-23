export class Report {
  public returnFile = '';
  public versionFrom = '';
  public versionTo = '';
  public changes: ReportChange[] = [];
}

export class ReportChange {
  public originversion = 0;
  public changeDescription = '';
  public changeUrl = '';
  public linesChanged: number[] = [];
}
