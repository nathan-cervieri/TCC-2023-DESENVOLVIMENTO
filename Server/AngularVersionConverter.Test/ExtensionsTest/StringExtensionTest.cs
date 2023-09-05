using AngularVersionConverter.Application.Extensions;
using System.Text.RegularExpressions;

namespace AngularVersionConverter.Test.ExtensionsTest
{
    public class StringExtensionTest
    {
        [Fact]
        public void StringExtension_IsMatchForString_ShoudldReturnTrue()
        {
            var regexString = @"\s+test";
            var matchString = "    test";

            matchString.IsMatchFor(regexString).Should().BeTrue();
        }

        [Fact]
        public void StringExtension_IsMatchForString_ShoudldReturnFalse()
        {
            var regexString = @"test\s+";
            var matchString = "    test";

            matchString.IsMatchFor(regexString).Should().BeFalse();
        }

        [Fact]
        public void StringExtension_IsMatchForRegex_ShoudldReturnTrue()
        {
            var regex = new Regex(@"\s+test");
            var matchString = "    test";

            matchString.IsMatchFor(regex).Should().BeTrue();
        }

        [Fact]
        public void StringExtension_IsMatchForRegex_ShoudldReturnfalse()
        {
            var regex = new Regex(@"test\s+");
            var matchString = "    test";

            matchString.IsMatchFor(regex).Should().BeFalse();
        }

        [Fact]
        public void StringExtension_IsNotMatchForRegex_ShoudldReturnTrue()
        {
            var regexString = @"test\s+";
            var matchString = "    test";

            matchString.IsNotMatchFor(regexString).Should().BeTrue();
        }

        [Fact]
        public void StringExtension_IsNotMatchForRegex_ShoudldReturnfalse()
        {
            var regexString = @"\s+test";
            var matchString = "    test";

            matchString.IsNotMatchFor(regexString).Should().BeFalse();
        }
    }
}
