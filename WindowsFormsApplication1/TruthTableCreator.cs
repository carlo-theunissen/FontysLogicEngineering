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
