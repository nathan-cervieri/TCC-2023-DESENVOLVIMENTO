using AngularVersionConverter.Application.Handlers;
using AngularVersionConverter.Domain.Entities;
using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Reports;

namespace AngularVersionConverter.Application.Interfaces
{
    public interface IConverterService
    {
        IEnumerable<AngularVersionEnum> GetAngularVersionEnums();
        Report GetAllOneTimeReports(AngularVersionEnum versionFrom, AngularVersionEnum versionTo);
        Report ConvertAngularFile(Stream streamToConvert, AngularVersionEnum versionFrom, AngularVersionEnum versionTo);
        string ConvertAngularLine(string stringToConvert, IEnumerable<VersionChange> versionChangeList, ReportBuilder reportBuilder);
    }
}
