using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Entities.VersionChange.ChangeReplace;
using AngularVersionConverter.Domain.Models.VersionChange.ChangeReplace;
using AngularVersionConverter.Models;
using AngularVersionConverterApplication.Interfaces.Repository;

namespace AngularVersionConverter.Infra.Repositories.Mocked
{
    public class VersionChangeRepository : IVersionChangeRepository
    {
        private static readonly IEnumerable<VersionChange> versionChangeList = new List<VersionChange>
        {
            new VersionChange
            {
                Id = new Guid(),
                Version = Models.AngularVersionEnum.Angular15,
                ChangeType = ChangeTypeEnum.SingleImportOriginChange,
                ChangeFinderRegexString = @"\s*XhrFactory\s*",
                FindReplaceList = new List<FindReplace>
                {
                    new FindReplace
                    {
                        Type = FindReplaceTypeEnum.Replace,
                        FindChangeString = @"{\s*XhrFactory\s*}",
                        WhatReplaceRegex = @"@angular/common/http",
                        ReplaceText = @"@angular/common",
                    },
                    new FindReplace
                    {
                        Type = FindReplaceTypeEnum.ReplaceAndNewLine,
                        FindChangeString = @"{.*\s*,\s*XhrFactory\s*.*}",
                        WhatReplaceRegex = @",\s*XhrFactory",
                        ReplaceText = @"",
                        NewLine = @"import { XhrFactory } from '@angular/common"
                    },
                    new FindReplace
                    {
                        Type = FindReplaceTypeEnum.ReplaceAndNewLine,
                        FindChangeString = @"{\s*XhrFactory\s*,.*}",
                        WhatReplaceRegex = @"XhrFactory\s*,\s*",
                        ReplaceText = @"",
                        NewLine = @"import { XhrFactory } from '@angular/common"
                    }
                },
                Description = "Simple import change"
            }
        };

        public IEnumerable<VersionChange> GetAll()
        {
            return versionChangeList;
        }

        public IEnumerable<VersionChange> GetVersionsFromTo(AngularVersionEnum versionFrom, AngularVersionEnum versionTo)
        {
            return versionChangeList.Where(vcl => vcl.Version > versionFrom && vcl.Version <= versionTo);
        }
    }
}
