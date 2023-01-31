namespace TextFirstUniqueCharSelector.Logic.Collections
{
    internal class UniqueOrderedValues<T> where T : notnull
    {
        private LinkedList<T> order = new();
        private Dictionary<T, LinkedListNode<T>?> uniquness = new();

        public int UniqueValuesCount => order.Count;

        public void Add(T value)
        {
            if (uniquness.TryGetValue(value, out var node))
            {
                if (node is not null)
                {
                    order.Remove(node);
                    uniquness[value] = null;
                }
                return;
            }

            uniquness[value] = order.AddLast(value);
        }

        public T First() => order.First();

        public void Clear()
        {
            if (order.Count != 0)
                order.Clear();

            if (uniquness.Count != 0)
                uniquness.Clear();
        }
    }
}
