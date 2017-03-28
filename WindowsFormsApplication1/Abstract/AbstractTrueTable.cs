using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.interfaces;




namespace WindowsFormsApplication1.Abstract
{
	public abstract class AbstractTrueTable : ITruthTable
	{
		protected IArgumentController _manager;
		protected IAsciiBaseOperator _operator;
		protected IParser _parser;

		public abstract byte[][] GetTable();
		public AbstractTrueTable(IParser parser)
		{
			_manager = parser.GetArgumentController();
			_operator = parser.GetOperator();
			_parser = parser;
		}
		protected bool? GetResults(ref bool[] data)
		{
			char[] names = _operator.GetArguments();
			for (int i = 0; i < data.Length; i++)
			{
				_manager.SetArgumentValue(names[i], data[i]);
			}
			return _operator.Result();
		}
		protected bool[][] GetAllOptions(int length)
		{
			List<bool[]> result = new List<bool[]>();

			//calculate the amount of different options 
			int num = (int)Math.Pow(2, length) - 1;

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
		public abstract string ToHex();

		public IParser GetParser()
		{
			return _parser;
		}
	}
}
