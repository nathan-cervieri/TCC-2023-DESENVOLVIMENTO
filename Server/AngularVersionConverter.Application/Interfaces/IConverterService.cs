using AngularVersionConverter.Application.Handlers;
using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Reports;
using AngularVersionConverter.Models;

namespace AngularVersionConverter.Application.Interfaces
{
    public interface IConverterService
    {
        public Report ConvertAngularFile(Stream streamToConvert, AngularVersionEnum versionFrom, AngularVersionEnum versionTo);
        public string ConvertAngularLine(string stringToConvert, IEnumerable<VersionChange> versionChangeList, ReportBuilder reportBuilder);
    }
}
