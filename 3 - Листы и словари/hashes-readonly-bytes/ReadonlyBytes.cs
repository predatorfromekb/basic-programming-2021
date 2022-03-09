using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace hashes
{
	public class ReadonlyBytes : IEnumerable<byte>
	{
		private readonly byte[] array;
		private readonly int hash;

		public ReadonlyBytes(params byte[] array)
		{
			this.array = array ?? throw new ArgumentNullException();
			hash = CalculateHashCode();
		}

		public int Length => array.Length;

		public IEnumerator<byte> GetEnumerator()
		{
			return ((IEnumerable<byte>) array).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public byte this[int index]
		{
			get
			{
				if (index >= Length)
					throw new IndexOutOfRangeException();
				return array[index];
			}
		}

		public override string ToString()
		{
			return "[" + string.Join(", ", array) + "]";
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != GetType())
			{
				return false;
			}

			var bytes = (ReadonlyBytes) obj;

			if (bytes.Length != Length)
			{
				return false;
			}

			for (int i = 0; i < Length; i++)
			{
				if (bytes[i] != array[i])
				{
					return false;
				}
			}

			return true;
		}

		private int CalculateHashCode()
		{
			var hashResult = array.Aggregate(2166136261, (current, b) => unchecked((current + b) * 16777619));
			return unchecked((int)hashResult);
		}

		public override int GetHashCode()
		{
			return hash;
		}
	}
}