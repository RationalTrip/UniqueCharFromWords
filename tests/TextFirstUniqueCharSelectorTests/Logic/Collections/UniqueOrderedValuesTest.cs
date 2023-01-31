using FluentAssertions;
using TextFirstUniqueCharSelector.Logic.Collections;

namespace TextFirstUniqueCharSelectorTests.Logic.Collections
{
    public class UniqueOrderedValuesTest
    {
        [Theory]
        [InlineData(1, 6, new int[] { 1, 2, 3, 4, 5, 6 })]
        [InlineData(4, 1, new int[] { 1, 2, 3, 4, 1, 2, 3 })]
        [InlineData(5, 1, new int[] { 1, 2, 5, 3, 4, 1, 2, 3, 4 })]
        [InlineData(8, 3, new int[] { 2, 3, 4, 2, 8, 1, 2, 3, 4, 10 })]
        [InlineData('a', 6, new char[] { 'a', 'b', 'c', 'd', 'e', 'f' })]
        [InlineData('b', 3, new char[] { 'a', 'b', 'e', 'd', 'e', 'f', 'a' })]
        [InlineData('f', 1, new char[] { 'a', 'b', 'e', 'b', 'e', 'f', 'a' })]
        public void UniqueOrderValues_CorrectWork<T>(T expectedResult, int expectedCount, T[] values) where T : notnull
        {
            //arrange
            var collection = new UniqueOrderedValues<T>();

            //act
            foreach (var item in values)
            {
                collection.Add(item);
            }

            var actualResult = collection.First();

            //assert
            collection.UniqueValuesCount.Should().Be(expectedCount);

            actualResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 1, 2, 3, 4 })]
        [InlineData(new int[] { 1, 2, 5, 1, 4, 1, 2, 5, 4 })]
        [InlineData(new int[] { 2, 3, 4, 2, 8, 2, 3, 4, 8 })]
        [InlineData(new int[] { })]
        [InlineData(new char[] { 'a', 'b', 'c', 'b', 'c', 'a' })]
        [InlineData(new char[] { 'a', 'b', 'e', 'd', 'e', 'd', 'a', 'b' })]
        [InlineData(new char[] { 'a', 'b', 'e', 'b', 'e', 'a', 'a' })]
        [InlineData(new char[] { })]
        public void UniqueOrderValues_NoneUniqueValues_ZeroCount<T>(T[] values) where T : notnull
        {
            //arrange
            var collection = new UniqueOrderedValues<T>();

            //act
            foreach (var item in values)
            {
                collection.Add(item);
            }

            //assert
            collection.UniqueValuesCount.Should().Be(0);
        }
    }
}
