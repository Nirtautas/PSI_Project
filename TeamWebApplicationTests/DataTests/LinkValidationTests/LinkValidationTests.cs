using TeamWebApplication.Models;

namespace TeamWebApplicationTests.DataTests.LinkValidationTests
{
    public class LinkValidationTests
    {
        [Theory]
        [InlineData("", "", "")]
        [InlineData("Hello, world!;; 1234.", "Hello world!; 1234", ",;.")]
        [InlineData(@"This, is a nice sentence.!\']; /. ;", @"This is a nice sentence.!\'] / ", ",;.;")]
        public void RemovePunctuation_GivingDifferentStrings_CorrectlyRemovesPunctuation(string textData, string expected, string expectedPunctuation)
        {
            Assert.Equal(expected, LinkValidation.RemovePunctuation(textData, out string punctuation));
            Assert.Equal(expectedPunctuation, punctuation);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("The quick brown fox", "The quick brown fox")]
        [InlineData("The https://www.youtube.com/watch?v=hN5X4kGhAtU&list=RD-hXlRYsHG1Q&index=7 fox",
            "The <a href=\"https://www.youtube.com/watch?v=hN5X4kGhAtU&list=RD-hXlRYsHG1Q&index=7\">https://www.youtube.com/watch?v=hN5X4kGhAtU&list=RD-hXlRYsHG1Q&index=7</a> fox")]
        [InlineData("The https://www.youtube.com/watch?v=hN5X4kGhAtU&list=RD-hXlRYsHG1Q&index=7. Fox.",
            "The <a href=\"https://www.youtube.com/watch?v=hN5X4kGhAtU&list=RD-hXlRYsHG1Q&index=7\">https://www.youtube.com/watch?v=hN5X4kGhAtU&list=RD-hXlRYsHG1Q&index=7</a>. Fox.")]
        [InlineData("The https://www.youtu. Fox.",
            "The https://www.youtu. Fox.")]
        public void ValidateAndReplaceLinks_GivingDifferentStrings_CorrectlyReplacesString(string textData, string expected)
        {
            Assert.Equal(expected, LinkValidation.ValidateAndReplaceLinks(textData));
        }
    }
}
