using AngularVersionConverter.Application.Extensions;
using AngularVersionConverter.Application.Interfaces;
using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Entities.VersionChange.ChangeReplace;
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

        private static string HandleVersionChange(string line, VersionChange versionChange)
        {
            if (line.IsNotMatchFor(versionChange.ChangeFinderRegexString))
            {
                return line;
            }

            return ApplyVersionChange(line, versionChange);
        }

        private static string ApplyVersionChange(string line, VersionChange change)
        {
            var changeType = change.ChangeType;
            if (changeType == ChangeTypeEnum.None)
            {
                throw new ArgumentException("VersionChange invalid", nameof(change));
            }
            if (changeType == ChangeTypeEnum.SingleImportOriginChange || changeType == ChangeTypeEnum.MultipleImportOriginChange)
            {
                return ApplyImportOriginChange(line, change.FindReplaceList);
            }

            throw new InvalidOperationException("Change type not yet supported");
        }

        private static string ApplyImportOriginChange(string line, IEnumerable<FindReplace> findReplaceChanges)
        {
            foreach (var findReplace in findReplaceChanges)
            {
                if (line.IsNotMatchFor(findReplace.FindChangeString))
                {
                    continue;
                }

                return ApplyFindReplaceImport(line, findReplace);
            }

            return line;
        }

        private static string ApplyFindReplaceImport(string line, FindReplace findReplace)
        {
            if (findReplace.Type == FindReplaceTypeEnum.Replace)
            {
                return HandleFindReplaceImportTypeReplace(line, findReplace);
            }
            if (findReplace.Type == FindReplaceTypeEnum.ReplaceAndNewLine)
            {
                return HandleFindReplaceImportTypeReplaceAndNewLine(line, findReplace);
            }
            if(findReplace.Type == FindReplaceTypeEnum.MultipleReplaceAndNewLine)
            {
                return HandleFindReplaceImportTypeMultipleReplaceAndNewLine(line, findReplace);
            }

            throw new InvalidOperationException();
        }

        private static string HandleFindReplaceImportTypeReplace(string line, FindReplace findReplace)
        {
            var regex = new Regex(findReplace.WhatReplaceRegex);
            return regex.Replace(line, findReplace.ReplaceText);
        }

        private static string HandleFindReplaceImportTypeReplaceAndNewLine(string line, FindReplace findReplace)
        {
            var regex = new Regex(findReplace.WhatReplaceRegex);
            var replacedLine = regex.Replace(line, findReplace.ReplaceText);

            var finalLineBuilder = new StringBuilder();
            finalLineBuilder.AppendLine(replacedLine);
            finalLineBuilder.Append(findReplace.NewLine);

            return finalLineBuilder.ToString();
        }

        private static string HandleFindReplaceImportTypeMultipleReplaceAndNewLine(string line, FindReplace findReplace)
        {
            var firstSplit = line.Split("{");
            var secondSplit = firstSplit[1].Split("}");
            var forSearchString = secondSplit[0];
            var separatedSearchString = forSearchString.Split(",");

            var regex = new Regex(findReplace.WhatReplaceRegex);
            var defaultLineImport = new List<string>();
            var separateLineImport = new List<string>();
            foreach(var import in separatedSearchString)
            {
                if(import.IsMatchFor(regex)) {
                    separateLineImport.Add(import.Trim());
                    continue;
                }

                defaultLineImport.Add(import.Trim());
            }

            var finalResult = new StringBuilder();
            finalResult.Append(firstSplit[0]);
            finalResult.Append("{ ");
            finalResult.Append(string.Join(", ", defaultLineImport));
            finalResult.Append(" }");
            finalResult.AppendLine(secondSplit[1]);
            finalResult.Append(findReplace.NewLine.Replace("{replaced}", string.Join(", ", separateLineImport)));

            return finalResult.ToString();
        }
    }
}
