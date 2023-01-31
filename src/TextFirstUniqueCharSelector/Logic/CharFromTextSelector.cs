using TextFirstUniqueCharSelector.Logic.Collections;

namespace TextFirstUniqueCharSelector.Logic
{
    public class CharFromTextSelector
    {
        private UniqueOrderedValues<char> uniqueOrderedChars = new();
        public void AddText(string text)
        {
            var wordEnumerator = new WordEnumerator(text);

            foreach (var word in wordEnumerator)
            {
                if (TryGetFirstUniqueChar(word, out var letter))
                    uniqueOrderedChars.Add(letter);
            }
        }

        public bool TryGetResult(out char firstUniqueChar)
        {
            if (uniqueOrderedChars.UniqueValuesCount == 0)
            {
                firstUniqueChar = char.MinValue;
                return false;
            }

            firstUniqueChar = uniqueOrderedChars.First();
            return true;
        }

        internal static bool TryGetFirstUniqueChar(ReadOnlySpan<char> word, out char result)
        {
            for (int i = 0; i < word.Length; i++)
            {
                char currentChar = word[i];

                bool containsBeforeCurrent = word.Slice(0, i).Contains(currentChar);
                bool containsAfterCurrent = word.Slice(i + 1).Contains(currentChar);

                if (!containsBeforeCurrent && !containsAfterCurrent)
                {
                    result = currentChar;
                    return true;
                }
            }

            result = char.MinValue;
            return false;
        }

        public void Clear() => uniqueOrderedChars.Clear();
    }
}
