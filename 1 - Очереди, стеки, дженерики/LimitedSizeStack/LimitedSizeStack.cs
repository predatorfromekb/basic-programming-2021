using System.Collections.Generic;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private readonly int limit;
        private readonly LinkedList<T> items;

        public LimitedSizeStack(int limit)
        {
            this.limit = limit;
            items = new LinkedList<T>();
        }

        public void Push(T item)
        {
            items.AddLast(item);
            if (items.Count > limit)
            {
                items.RemoveFirst();
            }
        }

        public T Pop()
        {
            var value = items.Last.Value;
            items.RemoveLast();
            return value;
        }

        public int Count => items.Count;
    }
}
