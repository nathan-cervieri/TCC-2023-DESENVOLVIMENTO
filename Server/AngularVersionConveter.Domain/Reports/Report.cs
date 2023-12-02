using AngularVersionConverter.Domain.Entities;

namespace AngularVersionConverter.Domain.Reports
{
    public class Report
    {
        public Report() { }

        public Report(AngularVersionEnum versionFrom = AngularVersionEnum.Angular15, AngularVersionEnum versionTo = AngularVersionEnum.Angular16)
        {
            VersionFrom = versionFrom;
            VersionTo = versionTo;
        }

        public IEnumerable<ReportChange> Changes { get; set; } = new List<ReportChange>();
        public AngularVersionEnum VersionFrom { get; set; }
        public AngularVersionEnum VersionTo { get; set; }
        public bool HasManualChange { get; set; }
        public bool HasAutomaticChange { get; set; }
        public string ReturnFile { get; set; } = "";
    }
}
