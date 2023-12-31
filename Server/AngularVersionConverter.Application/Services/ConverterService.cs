﻿using AngularVersionConverter.Application.Extensions;
using AngularVersionConverter.Application.Handlers;
using AngularVersionConverter.Application.Interfaces;
using AngularVersionConverter.Domain.Entities;
using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Entities.VersionChange.ChangeReplace;
using AngularVersionConverter.Domain.Reports;
using AngularVersionConverter.Infra.Interfaces;
using System.Text;
using System.Text.RegularExpressions;

namespace AngularVersionConverter.Application.Services
{
    public class ConverterService : IConverterService
    {
        private readonly IVersionChangeRepository versionChangeRepository;

        public ConverterService(IVersionChangeRepository versionChangeRepository)
        {
            this.versionChangeRepository = versionChangeRepository;
        }

        public IEnumerable<AngularVersionEnum> GetAngularVersionEnums()
        {
            var values = Enum.GetValues(typeof(AngularVersionEnum)).Cast<AngularVersionEnum>()
                .Where(versionEnum => versionEnum != AngularVersionEnum.None);
            return values;
        }

        public Report GetAllOneTimeReports(AngularVersionEnum versionFrom = AngularVersionEnum.Angular15,
            AngularVersionEnum versionTo = AngularVersionEnum.Angular16)
        {
            var reportBuilder = new ReportBuilder();
            var allOneTimeChanges = versionChangeRepository.GetStaticChangesFrom(versionFrom, versionTo);

            foreach (var change in allOneTimeChanges)
            {
                reportBuilder.AddChange(change.Description, change.Version, change.InformationUrl);
            }

            return reportBuilder.Build();
        }

        public Report ConvertAngularFile(Stream fileToConvert, AngularVersionEnum versionFrom = AngularVersionEnum.Angular15, AngularVersionEnum versionTo = AngularVersionEnum.Angular16)
        {
            var fileString = fileToConvert.ReadStreamToEnd();
            var separatedTsFileInLines = Regex.Split(fileString, "\r\n|\n|\r");
            var reportBuilder = new ReportBuilder(versionFrom, versionTo);

            var versionChanges = versionChangeRepository.GetDynamicChangesFromTo(versionFrom, versionTo);

            var finalFile = new StringBuilder();
            foreach (var line in separatedTsFileInLines)
            {
                finalFile.AppendLine(ConvertAngularLine(line, versionChanges, reportBuilder));
                reportBuilder.IncrementLine();
            }

            reportBuilder.AddFinishedFile(finalFile.ToString());
            return reportBuilder.Build();
        }

        public string ConvertAngularLine(string lineToConvert, IEnumerable<VersionChange> versionChangeList, ReportBuilder reportBuilder)
        {
            if (string.IsNullOrWhiteSpace(lineToConvert))
            {
                return string.Empty;
            }

            var finalLine = lineToConvert;
            foreach (var versionChange in versionChangeList)
            {
                finalLine = HandleVersionChange(finalLine, versionChange, reportBuilder);
            }

            return finalLine;
        }

        private static string HandleVersionChange(string line, VersionChange versionChange, ReportBuilder reportBuilder)
        {
            if (line.IsNotMatchFor(versionChange.ChangeFinderRegexString))
            {
                return line;
            }

            return ApplyVersionChange(line, versionChange, reportBuilder);
        }

        private static string ApplyVersionChange(string line, VersionChange change, ReportBuilder reportBuilder)
        {
            var changeType = change.ChangeType;
            if (changeType == ChangeTypeEnum.None)
            {
                throw new ArgumentException("VersionChange invalid", nameof(change));
            }
            if (changeType == ChangeTypeEnum.SingleImportOriginChange || changeType == ChangeTypeEnum.MultipleImportOriginChange)
            {
                return ImportHandler.ApplyImportOriginChange(line, change, reportBuilder);
            }
            if (changeType == ChangeTypeEnum.NoChangeOnlyWarn)
            {
                reportBuilder.AddChange(change.Description, change.Version);
                reportBuilder.ActivateHasManualChange();
                return line;
            }

            throw new InvalidOperationException("Change type not yet supported");
        }
    }
}
