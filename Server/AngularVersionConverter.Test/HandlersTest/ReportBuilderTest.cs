using AngularVersionConverter.Application.Handlers;
using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Reports;
using AngularVersionConverter.Models;

namespace AngularVersionConverter.Test.HandlersTest
{
    public class ReportBuilderTest
    {
        [Fact]
        public void ReportBuilder_AddChange_ShouldAddChangeToReport()
        {
            // Setup
            var reportBuilder = new ReportBuilder();

            // Act
            reportBuilder.AddChange("test", AngularVersionEnum.Angular15);
            var report = reportBuilder.Build();

            // Assert
            var reportChange = new ReportChange
            {
                ChangeDescription = "test",
                OriginVersion = AngularVersionEnum.Angular15,
                LinesChanged = new int[] { 1 }
            };

            report.Changes.Should().HaveCount(1);
            report.Changes.First().Should().BeEquivalentTo(reportChange);
        }

        [Fact]
        public void ReportBuilder_AddExtraLineChange_ShouldAddMultiLineChangeToReport()
        {
            // Setup
            var reportBuilder = new ReportBuilder();

            // Act
            reportBuilder.AddExtraLineChange("test", AngularVersionEnum.Angular15);
            var report = reportBuilder.Build();

            // Assert
            var reportChange = new ReportChange
            {
                ChangeDescription = "test",
                OriginVersion = AngularVersionEnum.Angular15,
                LinesChanged = new int[] { 1, 2 }
            };

            report.Changes.Should().HaveCount(1);
            report.Changes.First().Should().BeEquivalentTo(reportChange);
        }

        [Theory]
        [InlineData(FindReplaceTypeEnum.None)]
        [InlineData(FindReplaceTypeEnum.Replace)]
        public void ReportBuilder_AddChange_ShouldAddSimpleChangeIfTypeIsNotMultiline(FindReplaceTypeEnum findReplaceTypeEnum)
        {
            // Setup
            var reportBuilder = new ReportBuilder();

            // Act
            reportBuilder.AddChange("test", AngularVersionEnum.Angular15, findReplaceTypeEnum);
            var report = reportBuilder.Build();

            // Assert
            var reportChange = new ReportChange
            {
                ChangeDescription = "test",
                OriginVersion = AngularVersionEnum.Angular15,
                LinesChanged = new int[] { 1 }
            };

            report.Changes.Should().HaveCount(1);
            report.Changes.First().Should().BeEquivalentTo(reportChange);
        }

        [Theory]
        [InlineData(FindReplaceTypeEnum.NewLine)]
        [InlineData(FindReplaceTypeEnum.ReplaceAndNewLine)]
        [InlineData(FindReplaceTypeEnum.MultipleReplaceAndNewLine)]
        public void ReportBuilder_AddChange_ShouldAddLineToChangeIfTypeIsMultiLine(FindReplaceTypeEnum findReplaceTypeEnum)
        {
            // Setup
            var reportBuilder = new ReportBuilder();

            // Act
            reportBuilder.AddChange("test", AngularVersionEnum.Angular15, findReplaceTypeEnum);
            var report = reportBuilder.Build();

            // Assert
            var reportChange = new ReportChange
            {
                ChangeDescription = "test",
                OriginVersion = AngularVersionEnum.Angular15,
                LinesChanged = new int[] { 1, 2 }
            };

            report.Changes.Should().HaveCount(1);
            report.Changes.First().Should().BeEquivalentTo(reportChange);
        }

        [Fact]
        public void ReportBuilder_AddFile_ShouldAddFinishedFile()
        {
            // Setup
            var reportBuilder = new ReportBuilder();

            // Act
            reportBuilder.AddFinishedFile("test");
            var report = reportBuilder.Build();

            // Assert
            report.ReturnFile.Should().Be("test");
        }

        [Fact]
        public void ReportBuilder_AddLine_ShouldAddLineToChange()
        {
            // Setup
            var reportBuilder = new ReportBuilder();

            // Act
            reportBuilder.AddChange("test", AngularVersionEnum.Angular15);
            reportBuilder.IncrementLine();
            reportBuilder.AddChange("test 2", AngularVersionEnum.Angular15);
            var report = reportBuilder.Build();

            // Assert
            var reportChange1 = new ReportChange
            {
                ChangeDescription = "test",
                OriginVersion = AngularVersionEnum.Angular15,
                LinesChanged = new int[] { 1 }
            };
            var reportChange2 = new ReportChange
            {
                ChangeDescription = "test 2",
                OriginVersion = AngularVersionEnum.Angular15,
                LinesChanged = new int[] { 2 }
            };

            var reportChangeList = new List<ReportChange> { reportChange1, reportChange2 };

            report.Changes.Should().BeEquivalentTo(reportChangeList);
        }
    }
}
