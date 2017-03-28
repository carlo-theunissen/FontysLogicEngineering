using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Abstract;
using WindowsFormsApplication1.interfaces;
using WindowsFormsApplication1.Operators;

namespace WindowsFormsApplication1.Decorators
{
	public class DisjunctiveNormalDecorator : AbstractParser
	{
		private readonly ITruthTable _data;
		private readonly char[] _names;
		private readonly ArgumentsManager _argumentManager;

		private IAsciiBaseOperator _operator;


		public DisjunctiveNormalDecorator(ITruthTable table)
		{
			_data = table;
			_argumentManager = new ArgumentsManager();
			_names = table.GetParser().GetOperator().GetArguments();
			StartCalculating();
		}

		public override IArgumentController GetArgumentController()
		{
			return _argumentManager;
		}

		public override IAsciiBaseOperator GetOperator()
		{

			return _operator;
		}
		private void StartCalculating()
		{
			byte[][] table = _data.GetTable();
			List<IAsciiBaseOperator> outcome = new List<IAsciiBaseOperator>();

			foreach(byte[] row in table) {
				if (row[row.Length - 1] == 1)
				{
					IAsciiBaseOperator[] opers = new IAsciiBaseOperator[_names.Length];
					for (int x = 0; x < _names.Length; x++)
					{
						opers[x] = GetOperator(row[x], _names[x]);
					}
					outcome.Add(CombineOperators(opers, '&'));
				}
			}

			_operator =  CombineOperators(outcome.ToArray(), '|');

		}

		private IAsciiBaseOperator CombineOperators(IAsciiBaseOperator[] opers, char glueName)
		{
			return CalculateCombine(opers, 0, glueName);
		}
		private IAsciiBaseOperator CalculateCombine(IAsciiBaseOperator[] row, int index, char glueName)
		{
			//we are the last
			if (index + 1 == row.Length)
			{
				return row[index];
			}



			IAsciiBaseOperator glue = _operatorFactory.GetOperator(glueName, _argumentManager);
			glue.Instantiate(new IAsciiBaseOperator[] { row[index], CalculateCombine(row, index+1, glueName)});

			return glue;
		}
		private IAsciiBaseOperator GetOperator(byte value, char name) {
			switch (value) {
				case 0:
					IAsciiBaseOperator oper =  new NotOperator(_argumentManager);
					oper.Instantiate(new IAsciiBaseOperator[] { _argumentManager.RequestOperator(name) });
					return oper;

				case 1:
					return _argumentManager.RequestOperator(name);

				case 2:
					IAsciiBaseOperator not = new NotOperator(_argumentManager);
					not.Instantiate(new IAsciiBaseOperator[] { _argumentManager.RequestOperator(name) });

					IAsciiBaseOperator or = new OrOperator(_argumentManager);
					or.Instantiate(new IAsciiBaseOperator[] { _argumentManager.RequestOperator(name), not });

					return or;
			}
			return null;
		}
	}
}
