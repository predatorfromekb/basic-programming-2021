using System.Collections.Generic;
using System.Linq;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        private readonly LimitedSizeStack<ICommand<TItem>> commands;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            commands = new LimitedSizeStack<ICommand<TItem>>(limit);
        }

        public void AddItem(TItem item)
        {
            ExecuteAndPush(new AddCommand<TItem>(item));
        }

        public void RemoveItem(int index)
        {
            ExecuteAndPush(new RemoveCommand<TItem>(index));
        }

        private void ExecuteAndPush(ICommand<TItem> command)
        {
            command.Execute(Items);
            commands.Push(command);
        }

        public bool CanUndo()
        {
            return commands.Count > 0;
        }

        public void Undo()
        {
            var command = commands.Pop();
            command.Undo(Items);
        }
    }
    
    public interface ICommand<TItem>
    {
        void Execute(List<TItem> items);
        void Undo(List<TItem> items);
    }

    public class AddCommand<TItem> : ICommand<TItem>
    {
        private readonly TItem item;

        public AddCommand(TItem item)
        {
            this.item = item;
        }

        public void Execute(List<TItem> items)
        {
            items.Add(item);
        }

        public void Undo(List<TItem> items)
        {
            items.RemoveAt(items.Count - 1);
        }
    }
    
    public class RemoveCommand<TItem> : ICommand<TItem>
    {
        private TItem item;
        private readonly int index;

        public RemoveCommand(int index)
        {
            this.index = index;
        }

        public void Execute(List<TItem> items)
        {
            item = items.ElementAt(index);
            items.RemoveAt(index);
        }

        public void Undo(List<TItem> items)
        {
            items.Insert(index, item);
        }
    }
}
