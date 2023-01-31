namespace TextFirstUniqueCharSelector.Logic.Collections
{
    internal ref struct WordEnumerator
    {
        private int index;
        private ReadOnlySpan<char> text;

        public WordEnumerator(ReadOnlySpan<char> text)
        {
            this.text = text;

            index = 0;
            Current = ReadOnlySpan<char>.Empty;
        }

        public WordEnumerator(string text) : this(text.AsSpan()) { }

        public bool MoveNext()
        {
            int wordStartIndex = -1;
            int wordLength = 0;

            for (; index < text.Length; index++)
            {
                var currentChar = text[index];

                if (!IsLetter(currentChar))
                {
                    if (wordStartIndex >= 0) //Current word is already ended.
                        break;

                    continue; //Current word is not started.
                }

                if (wordStartIndex < 0)
                    wordStartIndex = index;

                wordLength++;
            }

            if (wordStartIndex < 0)
            {
                Current = ReadOnlySpan<char>.Empty;

                return false;
            }

            Current = text.Slice(wordStartIndex, wordLength);

            return true;
        }

        public ReadOnlySpan<char> Current { get; private set; }

        public WordEnumerator GetEnumerator() => this;

        private static bool IsLetter(char letter) => char.IsLetter(letter);
    }
}
