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
			}
			return result.ToArray();
		}
		public bool?[][] GetSimpleTable()
		{
			bool[][] fullTable = GetFullTable();
			foreach (bool[] check in fullTable)
			{
			}
		}
		private bool ResultFromOperator(bool?[] data)
		{
			char[] names = _operator.GetArguments();
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = data[i] == null ? false : data[i];
				_manager.SetArgumentValue(names[i], data[i]);
			}
			return _operator.Result();
		}
		private bool?[][] CheckRow(bool?[] data, int offset)
		{

			if (offset + 1 == data.Length)
			{
				//i'm the last
				return LastItemInRow(data, offset);
			}


			//collect all the different values
			List<bool?[]> calculated = new List<bool?[]> ();
			for (int i = 0; i < 2; i++)
			{
				data[offset] = i > 0;
				bool?[] copy = new bool?[data.Length];
				Array.Copy(data, copy, data.Length);
				bool?[][] outcome = CheckRow(copy, offset + 1);
				calculated.AddRange(outcome);
			}

			//store the result of the values in a dictionary
			Dictionary<bool?[], bool> values = new Dictionary<bool?[], bool>();
			foreach (bool?[] item in calculated)
			{
				values.Add(item, ResultFromOperator(item));
			}

			

			for (int i = 0; i < 2; i++)
			{
				KeyValuePair<bool?[], bool>? firstItem = null;

				foreach (KeyValuePair<bool?[], bool> keyValue in values) {
					if (keyValue.Value == (i > 0))
					{
						if (firstItem == null)
						{
							firstItem = keyValue;
						}
						else
						{
							keyValue.Key[offset] = null;
							firstItem.Value.Key[offset] = null;
						}
					}
				}
			}

			return values.Keys.ToArray();

			//lets check for stuff

		}
		private bool?[][] LastItemInRow(bool?[] data, int offset)
		{
			data[offset] = true;
			bool first = ResultFromOperator(data);
			data[offset] = false;
			bool second = ResultFromOperator(data);

			if (first == second)
			{
				data[offset] = null;
				bool?[][] array = { data };
				return array;
			}


			bool?[] secondResult = new bool?[data.Length];
			Array.Copy(data, secondResult, data.Length);

			secondResult[offset] = true;

			bool?[][] twoArray = { data, secondResult };
			return twoArray;
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
