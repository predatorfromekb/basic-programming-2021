using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		private readonly Dictionary<char, Action<IVirtualMachine>> actions 
			= new Dictionary<char, Action<IVirtualMachine>>();

		public VirtualMachine(string program, int memorySize)
		{
			Memory = new byte[memorySize];
			Instructions = program;
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			if (!actions.ContainsKey(symbol))
			{
				actions.Add(symbol, execute);
			}
		}

		public void Run()
		{
			while (InstructionPointer != Instructions.Length)
			{
				var instruction = Instructions[InstructionPointer];
				if (actions.ContainsKey(instruction))
				{
					actions[instruction](this);
				}
				InstructionPointer++;
			}
		}
	}
}