﻿using AngularVersionConverter.Application.Extensions;
using AngularVersionConverter.Application.Handlers;
using AngularVersionConverter.Application.Interfaces;
using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Entities.VersionChange.ChangeReplace;
using AngularVersionConverter.Domain.Reports;
using AngularVersionConverter.Models;
using AngularVersionConverterApplication.Interfaces.Repository;
using System.Text;

namespace AngularVersionConverter.Application.Services
{
    public class ConverterService : IConverterService
    {
        private readonly IVersionChangeRepository versionChangeRepository;

        public ConverterService(IVersionChangeRepository versionChangeRepository)
        {
            this.versionChangeRepository = versionChangeRepository;
        }

        public Report ConvertAngularFile(Stream fileToConvert, AngularVersionEnum versionFrom = AngularVersionEnum.Angular14, AngularVersionEnum versionTo = AngularVersionEnum.Angular15)
        {
            var fileString = fileToConvert.ReadStreamToEnd();
            var separatedTsFileInLines = fileString.Split('\n');
            var reportBuilder = new ReportBuilder(versionFrom, versionTo);

            var versionChanges = versionChangeRepository.GetVersionsFromTo(versionFrom, versionTo);

            var finalFile = new StringBuilder();
            foreach (var line in separatedTsFileInLines)
            {
                finalFile.Append(ConvertAngularLine(line, versionChanges, reportBuilder));
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

            throw new InvalidOperationException("Change type not yet supported");
        }
    }
}
