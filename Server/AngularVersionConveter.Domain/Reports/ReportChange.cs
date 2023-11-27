using AngularVersionConverter.Domain.Entities;

namespace AngularVersionConverter.Domain.Reports
{
    public class ReportChange
    {
        public IEnumerable<int> LinesChanged { get; set; } = new List<int>();
        public AngularVersionEnum OriginVersion { get; set; }
        public string ChangeDescription { get; set; } = string.Empty;
        public string ChangeURL { get; set; } = string.Empty;
    }
}
