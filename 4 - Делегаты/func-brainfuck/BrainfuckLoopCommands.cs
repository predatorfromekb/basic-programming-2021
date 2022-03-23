using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
        {
            Dictionary<int, int> pairs = null;
            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0) return;
                pairs = pairs ?? CreatePairs(b.Instructions);
                b.InstructionPointer = pairs[b.InstructionPointer];
            });
            vm.RegisterCommand(']', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0) return;
                pairs = pairs ?? CreatePairs(b.Instructions);
                b.InstructionPointer = pairs[b.InstructionPointer];
            });
        }

        private static Dictionary<int, int> CreatePairs(string instructions)
        {
            var opens = new Stack<int>();
            var pairs = new Dictionary<int, int>();
            for (var i = 0; i < instructions.Length; i++)
            {
                if (instructions[i] == '[') opens.Push(i);
                if (instructions[i] == ']')
                {
                    pairs[i] = opens.Pop();
                    pairs[pairs[i]] = i;
                }
            }
            return pairs;
        }
    }
}