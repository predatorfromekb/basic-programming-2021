using System;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b =>
			{
				write((char) b.Memory[b.MemoryPointer]);
			});
			vm.RegisterCommand('+', b =>
			{
				var value = b.Memory[b.MemoryPointer];
				b.Memory[b.MemoryPointer] = (byte)(value == byte.MaxValue ? byte.MinValue : value + 1);
			});
			vm.RegisterCommand('-', b =>
			{
				var value = b.Memory[b.MemoryPointer];
				b.Memory[b.MemoryPointer] = (byte)(value == byte.MinValue ? byte.MaxValue : value - 1);
			});
			vm.RegisterCommand(',', b =>
			{
				b.Memory[b.MemoryPointer] = (byte)read();
			});
			vm.RegisterCommand('>', b =>
			{
				b.MemoryPointer = (b.MemoryPointer + 1) % b.Memory.Length;
			});
			vm.RegisterCommand('<', b =>
			{
				b.MemoryPointer = (b.MemoryPointer == 0 ? b.Memory.Length - 1 : b.MemoryPointer - 1);
			});
			
			RegisterCharRangeCommand(vm, 'a', 'z');
			RegisterCharRangeCommand(vm, 'A', 'Z');
			RegisterCharRangeCommand(vm, '0', '9');
		}

		private static void RegisterCharRangeCommand(IVirtualMachine vm, char start, char end)
		{
			for (var i = start; i <= end; i++)
			{
				var c = i;
				vm.RegisterCommand(c, b =>
				{
					b.Memory[b.MemoryPointer] = (byte) c;
				});
			}
		}
	}
}