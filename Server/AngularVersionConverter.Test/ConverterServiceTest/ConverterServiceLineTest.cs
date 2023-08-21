﻿using AngularVersionConverter.Application.Services;
using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Models.VersionChange;
using AngularVersionConverter.Domain.Models.VersionChange.ChangeReplace;
using AngularVersionConverterApplication.Interfaces.Repository;
using Moq;

namespace AngularVersionConverter.Test.ConverterServiceTest
{
    public class ConverterServiceTest
    {
        private readonly Mock<IVersionChangeRepository> versionChangeRepository;
        private readonly ConverterService converterService;

        public ConverterServiceTest()
        {
            versionChangeRepository = new Mock<IVersionChangeRepository>();
            converterService = new ConverterService(versionChangeRepository.Object);
        }

        [Fact]
        public void LineIsEmpty_ShouldReturnEmptyString()
        {
            // Act
            var returnString = converterService.ConvertAngularLine(string.Empty, new List<VersionChange>());

            // Assert
            returnString.Should().Be(string.Empty);
        }

        [Fact]
        public void LineIsWhiteSpace_ShouldReturnEmptyString()
        {
            // Act
            var returnString = converterService.ConvertAngularLine("     ", new List<VersionChange>());

            // Assert
            returnString.Should().Be(string.Empty);
        }

        [Fact]
        public void VersionChangeIsInvalid_ShouldThrowInvalidArgumentException()
        {
            // Setup
            var versionChange = new VersionChange
            {
                ChangeType = ChangeTypeEnum.None
            };
            var versionChangeList = new List<VersionChange> { versionChange };

            // Act
            var act = () => converterService.ConvertAngularLine("test", versionChangeList);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ImportIsFromDifferentPackage_MustReplaceLocation()
        {
            // Setup
            var oldImportLine = "import { XhrFactory } from '@angular/common/http';";

            var versionChangeList = new List<VersionChange> { CreateVersionChangeXhrFactory() };

            // Act
            var updatedLine = converterService.ConvertAngularLine(oldImportLine, versionChangeList);

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

            var versionChangeList = new List<VersionChange> { CreateVersionChangeXhrFactory() };

            // Act
            var updatedLine = converterService.ConvertAngularLine(oldImportLine, versionChangeList);

            // Assert
            var newLine = "import { " + extraImports + " } from '@angular/common/http\r\nimport { XhrFactory } from '@angular/common";
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
    }
}
