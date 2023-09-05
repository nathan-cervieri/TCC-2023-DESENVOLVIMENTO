using AngularVersionConverter.Application.Handlers;
using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Entities.VersionChange.ChangeReplace;
using AngularVersionConverter.Domain.Models.VersionChange.ChangeReplace;

namespace AngularVersionConverter.Test.HandlersTest
{
    public class ImportHandlerTest
    {

        [Fact]
        public void ImportIsFromDifferentPackage_MustReplaceLocation()
        {
            // Setup
            var oldImportLine = "import { XhrFactory } from '@angular/common/http';";

            var versionChange = CreateVersionChangeXhrFactory();

            // Act
            var updatedLine = ImportHandler.ApplyImportOriginChange(oldImportLine, versionChange, new ReportBuilder());

            // Assert
            var newLine = "import { XhrFactory } from '@angular/common';";
            updatedLine.Should().Be(newLine);
        }

        [Theory]
        [InlineData("import { XhrFactory, test } from '@angular/common/http", "test")]
        [InlineData("import { test, XhrFactory } from '@angular/common/http", "test")]
        [InlineData("import { test, XhrFactory, test1 } from '@angular/common/http", "test, test1")]
        public void ImportIsFromDifferentPackage_MustCreateNewLine(string testCase, string extraImports)
        {
            // Setup
            var oldImportLine = testCase;
            var versionChange = CreateVersionChangeXhrFactory();

            // Act
            var updatedLine = ImportHandler.ApplyImportOriginChange(oldImportLine, versionChange, new ReportBuilder());

            // Assert
            var newLine = "import { " + extraImports + " } from '@angular/common/http\r\nimport { XhrFactory } from '@angular/common";
            updatedLine.Should().Be(newLine);
        }

        [Theory]
        [InlineData("makeStateKey, StateKey, TransferState")]
        [InlineData("makeStateKey, StateKey")]
        [InlineData("makeStateKey, TransferState")]
        [InlineData("StateKey, TransferState")]
        [InlineData("makeStateKey")]
        [InlineData("StateKey")]
        [InlineData("TransferState")]
        public void MultipleImportIsFromDifferentPackage_MustReplaceLine(string testCase)
        {
            // Setup
            var baseLine = "import { " + testCase + "} from '@angular/platform-browser'";
            var versionChange = CreateVersionChangeAngularPlatformBrowser();

            // Act
            var updatedLine = ImportHandler.ApplyImportOriginChange(baseLine, versionChange, new ReportBuilder());

            // Assert
            var newLine = "import { " + testCase + "} from '@angular/core'";
            updatedLine.Should().Be(newLine);
        }

        [Theory]
        [InlineData("test, makeStateKey, test, StateKey, TransferState", "test, test", "makeStateKey, StateKey, TransferState")]
        [InlineData("makeStateKey, StateKey, test", "test", "makeStateKey, StateKey")]
        [InlineData("test, makeStateKey, TransferState", "test", "makeStateKey, TransferState")]
        public void MultipleImportIsFromDifferentPackage_MustReplaceAndNewLine(string testCase, string baseResult, string newLineResult)
        {
            // Setup
            var baseLine = "import { " + testCase + " } from '@angular/platform-browser'";
            var versionChange = CreateVersionChangeAngularPlatformBrowser();

            // Act
            var updatedLine = ImportHandler.ApplyImportOriginChange(baseLine, versionChange, new ReportBuilder());

            // Assert
            var newLine = "import { " + baseResult + " } from '@angular/platform-browser'\r\nimport { " + newLineResult + " } from '@angular/core'";
            updatedLine.Should().Be(newLine);
        }

        public static VersionChange CreateVersionChangeXhrFactory()
        {
            return new VersionChange
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
            };
        }

        public static VersionChange CreateVersionChangeAngularPlatformBrowser()
        {
            return new VersionChange
            {
                Id = new Guid(),
                Version = Models.AngularVersionEnum.Angular15,
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
