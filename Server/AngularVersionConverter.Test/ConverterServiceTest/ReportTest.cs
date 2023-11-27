using AngularVersionConverter.Application.Services;
using AngularVersionConverter.Domain.Entities;
using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Entities.VersionChange.ChangeReplace;
using AngularVersionConverter.Domain.Models.VersionChange.ChangeReplace;
using AngularVersionConverter.Infra.Interfaces;
using Moq;

namespace AngularVersionConverter.Test.ConverterServiceTest
{
    public class ReportTest
    {
        private readonly Mock<IVersionChangeRepository> versionChangeRepository;
        private readonly ConverterService converterService;

        public ReportTest()
        {
            versionChangeRepository = new Mock<IVersionChangeRepository>();
            converterService = new ConverterService(versionChangeRepository.Object);
        }

        public static VersionChange CreateVersionChangeXhrFactory()
        {
            return new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
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
            };
        }

        public static VersionChange CreateVersionChangeAngularPlatformBrowser()
        {
            return new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType = ChangeTypeEnum.MultipleImportOriginChange,
                ChangeFinderRegexString = @"^import\s*{.*(makeStateKey|StateKey|TransferState)+.*}\s*from\s*'@angular\/platform-browser'$",
                FindReplaceList = new List<FindReplace>
                {
                    new FindReplace
                    {
                        Type = FindReplaceTypeEnum.Replace,
                        FindChangeString = @"{\s*(((StateKey|makeStateKey|TransferState),?)+\s*)+\s*}",
                        WhatReplaceRegex = @"@angular/platform-browser",
                        ReplaceText = @"@angular/core",
                    },
                    new FindReplace
                    {
                        Type = FindReplaceTypeEnum.MultipleReplaceAndNewLine,
                        FindChangeString = @"{(.*,\s*)?(makeStateKey|StateKey|TransferState)+\s*(,.*)?}",
                        WhatReplaceRegex = @"makeStateKey|StateKey|TransferState",
                        ReplaceText = @"",
                        NewLine = @"import { {replaced} } from '@angular/core'"
                    }
                },
                Description = "Complex import change"
            };
        }
    }
}
