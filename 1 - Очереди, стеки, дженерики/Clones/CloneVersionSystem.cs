using System.Collections.Generic;

namespace Clones
{
    public class CloneVersionSystem : ICloneVersionSystem
    {
        private readonly List<Clone> clones = new List<Clone>()
        {
            new Clone()
        };
		
        public string Execute(string query)
        {
            var args = query.Split();
            var command = args[0];
            var cloneNumber = int.Parse(args[1]);
            var index = cloneNumber - 1;
            switch (command)
            {
                case "learn":
                    clones[index].Learn(int.Parse(args[2]));
                    break;
                case "rollback":
                    clones[index].Rollback();
                    break;
                case "relearn":
                    clones[index].Relearn();
                    break;
                case "clone":
                    clones.Add(clones[index].CreateNew());
                    break;
                case "check":
                    return clones[index].Check();
            }

            return null;
        }

        class Clone
        {
            private Command learnedCommand;
            private Command rollbackCommand;

            public Clone()
            {
            }
			
            private Clone(Command learnedCommand, Command rollbackCommand)
            {
                this.learnedCommand = learnedCommand;
                this.rollbackCommand = rollbackCommand;
            }


            public void Learn(int value)
            {
                learnedCommand = new Command
                {
                    Value = value,
                    Previous = learnedCommand,
                };
            }
			
            public void Rollback()
            {
                rollbackCommand = new Command
                {
                    Value = learnedCommand.Value,
                    Previous = rollbackCommand,
                };
                learnedCommand = learnedCommand.Previous;
            }
			
            public void Relearn()
            {
                learnedCommand = new Command
                {
                    Value = rollbackCommand.Value,
                    Previous = learnedCommand,
                };
                rollbackCommand = rollbackCommand.Previous;
            }

            public string Check()
            {
                return learnedCommand?.Value.ToString() ?? "basic";
            }
			
            public Clone CreateNew()
            {
                return new Clone(learnedCommand, rollbackCommand);
            }
        }
    }
	
    public class Command
    {
        public int Value { get; set; }
        public Command Previous { get; set; }
    }
}