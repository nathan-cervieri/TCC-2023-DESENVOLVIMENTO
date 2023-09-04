using AngularVersionConverter.Models;

namespace AngularVersionConverter.Domain.Reports
{
    public class Report
    {
        public Report() { }

        public Report(AngularVersionEnum versionFrom = AngularVersionEnum.Angular14, AngularVersionEnum versionTo = AngularVersionEnum.Angular15)
        {
            VersionFrom = versionFrom;
            VersionTo = versionTo;
        }

        public IEnumerable<ReportChange> Changes { get; set; } = new List<ReportChange>();
        public AngularVersionEnum VersionFrom { get; set; }
        public AngularVersionEnum VersionTo { get; set; }
        public string ReturnFile { get; set; } = "";
    }
}
