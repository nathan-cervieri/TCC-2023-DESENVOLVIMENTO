using AngularVersionConverter.Application.Extensions;
using AngularVersionConverter.Application.Interfaces;
using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Models.VersionChange;
using AngularVersionConverter.Domain.Models.VersionChange.ChangeReplace;
using AngularVersionConverter.Models;
using AngularVersionConverterApplication.Interfaces.Repository;
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

        public string ConvertAngular(Stream fileToConvert, AngularVersionEnum versionFrom = AngularVersionEnum.Angular14, AngularVersionEnum versionTo = AngularVersionEnum.Angular15)
        {
            var newFile = new StringBuilder();
            var fileString = fileToConvert.ReadStreamToEnd();
            var separatedTsFileInFunctions = fileString.Split('\n').SelectMany(line => line.Split(";"));

            return newFile.ToString();
        }

        public string ConvertAngularLine(string lineToConvert, IEnumerable<VersionChange> versionChangeList)
        {
            if (string.IsNullOrWhiteSpace(lineToConvert))
            {
                return string.Empty;
            }

            var finalLine = lineToConvert;
            foreach (var versionChange in versionChangeList)
            {
                finalLine = HandleVersionChange(finalLine, versionChange);
            }

            return finalLine;
        }

        private string HandleVersionChange(string line, VersionChange versionChange)
        {
            if (line.IsNotMatchFor(versionChange.ChangeFinderRegexString))
            {
                return line;
            }

            return ApplyVersionChange(line, versionChange);
        }

        private string ApplyVersionChange(string line, VersionChange change)
        {
            if (change.ChangeType == ChangeTypeEnum.None)
            {
                throw new ArgumentException("VersionChange invalid", nameof(change));
            }
            if (change.ChangeType == ChangeTypeEnum.SingleImportOriginChange)
            {
                return ApplySingleImportOriginChange(line, change.FindReplaceList);
            }

            throw new InvalidOperationException("Change type not yet supported");
        }

        private static string ApplySingleImportOriginChange(string line, IEnumerable<FindReplace> findReplaceChanges)
        {
            foreach (var findReplace in findReplaceChanges)
            {
                if (line.IsNotMatchFor(findReplace.FindChangeString))
                {
                    continue;
                }

                return ApplyFindReplace(line, findReplace);
            }

            return line;
        }

        private static string ApplyFindReplace(string line, FindReplace findReplace)
        {
            if (findReplace.Type == FindReplaceTypeEnum.Replace)
            {
                return HandleFindReplaceTypeReplace(line, findReplace);
            }
            if (findReplace.Type == FindReplaceTypeEnum.ReplaceAndNewLine)
            {
                return HandleFindReplaceTypeReplaceAndNewLine(line, findReplace);
            }

            throw new InvalidOperationException();
        }

        private static string HandleFindReplaceTypeReplace(string line, FindReplace findReplace)
        {
            var regex = new Regex(findReplace.WhatReplaceRegex);
            return regex.Replace(line, findReplace.ReplaceText);
        }

        private static string HandleFindReplaceTypeReplaceAndNewLine(string line, FindReplace findReplace)
        {
            var regex = new Regex(findReplace.WhatReplaceRegex);
            var replacedLine = regex.Replace(line, findReplace.ReplaceText);

            var finalLineBuilder = new StringBuilder();
            finalLineBuilder.AppendLine(replacedLine);
            finalLineBuilder.Append(findReplace.NewLine);

            return finalLineBuilder.ToString();
        }
    }
}
