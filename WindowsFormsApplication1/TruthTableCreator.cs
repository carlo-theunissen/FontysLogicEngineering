using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.interfaces;
using System.Diagnostics;
namespace WindowsFormsApplication1
{
	class TruthTableCreator
	{
		private ArgumentsManager _manager;
		private IAsciiBaseOperator _operator;

		public TruthTableCreator(StringParser parser)
		{
			_manager = parser.ArgumentManager;
			_operator = parser.ToOperator();
		}
		public bool[][] GetFullTable()
		{
			char[] names = _operator.GetArguments();

			List<bool[]> result = new List<bool[]>();
			foreach (bool[] data in GetAllOptions(names.Length))
			{
				bool[] array = new bool[data.Length + 1];
				for (int i = 0; i < data.Length; i++)
				{
					_manager.SetArgumentValue(names[i], data[i]);
					array[i] = data[i];
				}
				array[data.Length] = _operator.Result();
				result.Add(array);
			}
			return result.ToArray();
		}
		/**
		 * Thanks to http://www.pvladov.com/2012/05/decimal-to-arbitrary-numeral-system.html 
		 */ 
		private byte[] DecimalToArbitrarySystem(long decimalNumber, int radix, int length)
		{

			if (radix < 2 || radix > 10)
				throw new ArgumentException("The radix must be >= 2 and <= 10");

			if (decimalNumber == 0)
			{
			
				return new byte[length];
			}


			long currentNumber = Math.Abs(decimalNumber);
			int index = 0;
			byte[] output = new byte[length];

			while (currentNumber != 0 && index < length)
			{
				int remainder = (int)(currentNumber % radix);
				output[index++] = (byte) remainder;
				currentNumber = currentNumber / radix;
			}

			return output;
		}
		private byte[][] GetThirthBaseOptions(int length)
		{
			int num = (int)Math.Pow(3, length) - 1;
			byte[][] output = new byte[num + 1][];
			for (int i = 0; i <= num; i++)
			{
				output[i] = DecimalToArbitrarySystem(i, 3, length);
			}
			return output;
		}
		private bool? GetResults(ref byte[] row)
		{
			bool? result = null;
			bool clean = true;
			for (int i = 0; i < row.Length; i++)
			{
				if (row[i] == 2)
				{
					clean = false;
					for (int x = 0; x < 2; x++)
					{
						row[i] = (byte) x;
						byte[] clone = (byte[])row.Clone();
						bool? outcome = GetResults(ref clone);
						if (outcome == null || (result != null && result != outcome))
						{
							return null;
						}
						result = outcome.Value;
					}
				}
			}

			if (!clean)
			{
				return result;
			}

			char[] names = _operator.GetArguments();
			for (int i = 0; i < row.Length; i++)
			{
				_manager.SetArgumentValue(names[i], row[i] != 0);
			}
			return _operator.Result();
		}
		private void GetSuccessFailList(out ICollection<byte[]> success, out ICollection<byte[]> fail, ref byte[][] data)
		{
			success = new List<byte[]>();
			fail = new List<byte[]>();
			foreach (byte[] items in data)
			{
				byte[] clone = (byte[])items.Clone();
				bool? outcome = GetResults(ref clone);
				if (outcome != null) {
					if (outcome.Value)
					{
						success.Add(items);
					}
					else
					{
						fail.Add(items);
					}
				}
			}
		}
		private int[] GetDifferIndexes(byte[] array1, byte[] array2)
		{
			List<int> indexes = new List<int>();
			for (int i = 0; i < array1.Length; i++)
			{
				if (!array1[i].Equals(array2[i]))
				{
					indexes.Add(i);
				}
			}
			return indexes.ToArray();
		}
		private bool ContainsItemInList<T>(T[] item, ref ICollection<T[]> collection)
		{
			foreach (T[] check in collection)
			{
				bool found = true;
				for (int i = 0; i < item.Length; i++)
				{
					if (!item[i].Equals(check.ElementAt(i)))
					{
						found = false;
						break;
					}
				}

				if (found)
				{
					return true;
				}
			}
			return false;
		}
		
		private byte[][] SimplfyList(byte[][] data) {
			ICollection<byte[]> newData = new List<byte[]>();
			ICollection<byte[]> used = new List<byte[]>(); 
			foreach (byte[] current in data)
			{
				if (!ContainsItemInList(current, ref used))
				{
					foreach (byte[] check in data)
					{
						if (!ContainsItemInList(check, ref used))
						{
							int[] differ = GetDifferIndexes(current, check);
							if (differ.Length == 1)
							{
								current[differ[0]] = 2;
								newData.Add(current);
								used.Add(current);
								used.Add(check);
								break;
							}
						}
					}
				}
			}
			foreach (byte[] current in data)
			{
				if (!ContainsItemInList(current, ref used))
				{
					newData.Add(current);
				}
			}
			if (data.Length == newData.Count)
			{
				return newData.ToArray();
			}
			return SimplfyList(newData.ToArray());
		}
		public byte[][] GetSimpleTable()
		{
			ICollection<byte[]> success;
			ICollection<byte[]> fail;

			byte[][] data = GetThirthBaseOptions(_operator.GetArguments().Length);

			GetSuccessFailList(out success, out fail, ref data);

			List<byte[]> result = new List<byte[]>();
			for (int i = 0; i < 2; i++)
			{

				foreach (byte[] items in SimplfyList(i == 0 ? fail.ToArray() : success.ToArray()))
				{
					byte[] array = new byte[items.Length + 1];
					Array.Copy(items, array, items.Length);
					array[items.Length] = (byte) i;
					result.Add(array);
				}
			}

			return result.ToArray();

		}
		private bool[][] GetAllOptions(int length)
		{
			List<bool[]> result = new List<bool[]>();

			//calculate the amount of different options 
			int num = (int) Math.Pow(2, length) -1;

			for (int i = 0; i <= num; i++)
			{
				bool[] array = new bool[length];
				for (int pos = 0; pos < length; pos++)
				{
					array[pos] = ((i & (1 << pos)) > 0);
				} 
				result.Add(array);
			}
			return result.ToArray();
		}
	}
}
