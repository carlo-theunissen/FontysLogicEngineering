using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.interfaces;
using System.Diagnostics;
using WindowsFormsApplication1.Utils;

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


		private bool? GetResults(ref bool[] data)
		{
			char[] names = _operator.GetArguments();
			for (int i = 0; i < data.Length; i++)
			{
				_manager.SetArgumentValue(names[i], data[i]);
			}
			return _operator.Result();
		}
		private void GetSuccessFailList(out ICollection<bool[]> success, out ICollection<bool[]> fail, ref bool[][] data)
		{
			success = new List<bool[]>();
			fail = new List<bool[]>();
			foreach (bool[] items in data)
			{
				bool[] clone = (bool[])items.Clone();
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
		
		private byte[][] TransformArray(bool[][] data)
		{
			byte[][] result = new byte[data.Length][];
			for (int i = 0; i < data.Length; i++)
			{
				result[i] = new byte[data[i].Length]; 
				for (int j = 0; j < data[i].Length; j++)
				{
					
					result[i][j] = (byte)(data[i][j] ? 1 : 0);
				}
			}
			return result;
		}
		private byte[][] SimplfyList(byte[][] data) {
			ICollection<byte[]> newData = new List<byte[]>();
			byte[][] clonedData = (byte[][] ) data.Clone();

			foreach (byte[] current in data)
			{
				foreach (byte[] check in data)
				{
					int[] differ = ArrayUtils.GetDifferIndexes(current, check);
					
					if (differ.Length == 1)
					{
						byte[] temp = (byte[]) check.Clone();
						temp[differ[0]] = 2;
						if (!ArrayUtils.ContainsArrayInList(temp, ref newData))
						{
							newData.Add(temp);
						}
					}
				}
			}

			if (newData.Count == 0)
			{
				return data;
			}
			return SimplfyList(newData.ToArray());
		}
		public byte[][] GetSimpleTable()
		{
			ICollection<bool[]> success;
			ICollection<bool[]> fail;

			bool[][] data = GetAllOptions(_operator.GetArguments().Length);

			GetSuccessFailList(out success, out fail, ref data);

			List<byte[]> result = new List<byte[]>();
			for (int i = 0; i < 2; i++)
			{
				byte[][] collection = i == 0 ? TransformArray(fail.ToArray()) : TransformArray(success.ToArray());

				foreach (byte[] items in SimplfyList(collection))
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
