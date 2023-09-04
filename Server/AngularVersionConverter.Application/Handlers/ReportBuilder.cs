using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Reports;
using AngularVersionConverter.Models;

namespace AngularVersionConverter.Application.Handlers
{
    public class ReportBuilder
    {
        private readonly Report report;
        private int line = 1;
        private readonly List<ReportChange> changes = new();

        public ReportBuilder(AngularVersionEnum versionFrom = AngularVersionEnum.Angular14, AngularVersionEnum versionTo = AngularVersionEnum.Angular15)
        {
            report = new Report(versionFrom, versionTo);
        }

        public ReportBuilder AddChange(string change, AngularVersionEnum originVersion, FindReplaceTypeEnum findReplaceType)
        {
            if (findReplaceType == FindReplaceTypeEnum.NewLine
                || findReplaceType == FindReplaceTypeEnum.ReplaceAndNewLine
                || findReplaceType == FindReplaceTypeEnum.MultipleReplaceAndNewLine)
            {
                return AddExtraLineChange(change, originVersion);
            }

            return AddChange(change, originVersion);
        }

        public ReportBuilder AddChange(string change, AngularVersionEnum originVersion)
        {
            var reportChange = new ReportChange
            {
                ChangeDescription = change,
                OriginVersion = originVersion,
                LinesChanged = new int[] { line }
            };

            changes.Add(reportChange);
            return this;
        }

        public ReportBuilder AddExtraLineChange(string change, AngularVersionEnum originVersion)
        {
            var lines = new int[] { line, line + 1 };
            IncrementLine();

            var reportChange = new ReportChange
            {
                ChangeDescription = change,
                OriginVersion = originVersion,
                LinesChanged = lines
            };

            changes.Add(reportChange);
            return this;
        }

        public ReportBuilder AddFinishedFile(string file)
        {
            report.ReturnFile = file;
            return this;
        }

        public ReportBuilder IncrementLine()
        {
            line += 1;
            return this;
        }

        public Report Build()
        {
            report.Changes = changes;
            return report;
        }
    }
}
