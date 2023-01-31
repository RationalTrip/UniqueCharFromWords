using FluentAssertions;
using TextFirstUniqueCharSelector.Logic;

namespace TextFirstUniqueCharSelectorTests.Logic
{
    public class CharFromTextSelectorTest
    {
        [Theory]
        [InlineData("word", true, 'w')]
        [InlineData("tested", true, 's')]
        [InlineData("random", true, 'r')]
        [InlineData("membership", true, 'b')]
        [InlineData("Tost", true, 'T')]
        [InlineData("calistetics", true, 'a')]
        [InlineData("tetateta", false, char.MinValue)]
        [InlineData("temmet", false, char.MinValue)]
        [InlineData("SomeoSme", false, char.MinValue)]
        public void TryGetFirstUniqueChar_Word_ReturnResult(string word,
            bool expectedResultExist,
            char expectedResult)
        {
            //act
            var actualResultExist = CharFromTextSelector.TryGetFirstUniqueChar(word.AsSpan(),
                out var actualResult);

            //assert
            actualResultExist.Should().Be(expectedResultExist);

            if (expectedResultExist)
            {
                actualResult.Should().Be(expectedResult);
            }
        }

        [Fact]
        public void CharFromTextSelectorTest_TestData_ExpectedResult()
        {
            //arrange
            var charFromTextSelector = new CharFromTextSelector();

            //act
            charFromTextSelector.AddText(TestText);

            charFromTextSelector.TryGetResult(out var actualResult);

            //assert
            actualResult.Should().Be(TestResult);
        }

        private static string TestText => "The Tao gave birth to machine language.  Machine language gave birth\n" +
            "to the assembler.\n" +
            "The assembler gave birth to the compiler.  Now there are ten thousand\n" +
            "languages.\n" +
            "Each language has its purpose, however humble.  Each language\n" +
            "expresses the Yin and Yang of software.  Each language has its place within\n" +
            "the Tao.\n" +
            "But do not program in COBOL if you can avoid it.\n" +
            "        -- Geoffrey James, \"The Tao of Programming\"\n";

        private static char TestResult => 'm';
    }
}
