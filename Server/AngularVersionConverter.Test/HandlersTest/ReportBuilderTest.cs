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
            reportBuilder.AddChange("teste", AngularVersionEnum.Angular15);
            var report = reportBuilder.Build();

            // Assert
            var reportChange = new ReportChange
            {
                ChangeDescription = "teste",
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
            reportBuilder.AddExtraLineChange("teste", AngularVersionEnum.Angular15);
            var report = reportBuilder.Build();

            // Assert
            var reportChange = new ReportChange
            {
                ChangeDescription = "teste",
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
            reportBuilder.AddChange("teste", AngularVersionEnum.Angular15, findReplaceTypeEnum);
            var report = reportBuilder.Build();

            // Assert
            var reportChange = new ReportChange
            {
                ChangeDescription = "teste",
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
            reportBuilder.AddChange("teste", AngularVersionEnum.Angular15, findReplaceTypeEnum);
            var report = reportBuilder.Build();

            // Assert
            var reportChange = new ReportChange
            {
                ChangeDescription = "teste",
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
            reportBuilder.AddFinishedFile("teste");
            var report = reportBuilder.Build();

            // Assert
            report.ReturnFile.Should().Be("teste");
        }

        [Fact]
        public void ReportBuilder_AddLine_ShouldAddLineToChange()
        {
            // Setup
            var reportBuilder = new ReportBuilder();

            // Act
            reportBuilder.AddChange("teste", AngularVersionEnum.Angular15);
            reportBuilder.IncrementLine();
            reportBuilder.AddChange("teste 2", AngularVersionEnum.Angular15);
            var report = reportBuilder.Build();

            // Assert
            var reportChange1 = new ReportChange
            {
                ChangeDescription = "teste",
                OriginVersion = AngularVersionEnum.Angular15,
                LinesChanged = new int[] { 1 }
            };
            var reportChange2 = new ReportChange
            {
                ChangeDescription = "teste 2",
                OriginVersion = AngularVersionEnum.Angular15,
                LinesChanged = new int[] { 2 }
            };

            var reportChangeList = new List<ReportChange> { reportChange1, reportChange2 };

            report.Changes.Should().BeEquivalentTo(reportChangeList);
        }
    }
}
