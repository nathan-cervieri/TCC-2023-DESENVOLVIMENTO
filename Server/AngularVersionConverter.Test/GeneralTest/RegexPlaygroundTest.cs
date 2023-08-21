using System.Text.RegularExpressions;

namespace AngularVersionConverter.Test.GeneralTest
{
    public class RegexPlaygroundTest
    {
        [Fact]
        public void RegexMustReplaceRegexText_ShouldWork()
        {
            // Setup
            var regex = new Regex(@"\s*text\s*");
            var wrongTextString = "       text    ";

            // Act
            var newTextString = regex.Replace(wrongTextString, "text");

            newTextString.Should().Be("text");
        }
    }
}
