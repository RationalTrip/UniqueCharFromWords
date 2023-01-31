using FluentAssertions;
using TextFirstUniqueCharSelector.Logic.Collections;

namespace TextFirstUniqueCharSelectorTests.Logic.Collections
{
    public class WordEnumeratorTest
    {
        [Theory]
        [InlineData("Some test text, \"you\" would \'like\' to test. Here. ",
            new string[] { "Some", "test", "text", "you", "would", "like", "to", "test", "Here" })]
        [InlineData("While $you@\" are \'  \"  coding., >< you<> should? can./, use<> another $$#@!*&)#%&*@%  monitor  for  googling.",
            new string[] { "While", "you", "are", "coding", "you", "should", "can", "use", "another", "monitor", "for", "googling" })]
        [InlineData(", \"\" \'$$#@!*&)#%&*@%\' . . ",
            new string[] { })]
        [InlineData("",
            new string[] { })]
        public void WordEnumeration_Success(string text, string[] expectedWords)
        {
            var wordEnumerator = new WordEnumerator(text);

            int index = 0;

            foreach (var word in wordEnumerator)
            {
                word.ToString().Should().Be(expectedWords[index]);
                index++;
            }

            index.Should().Be(expectedWords.Length,
                "Was read only {0} words, expected {1} words",
                index,
                expectedWords.Length);
        }
    }
}
