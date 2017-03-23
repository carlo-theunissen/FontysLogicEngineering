using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.interfaces;
using WindowsFormsApplication1.Operators;
namespace WindowsFormsApplication1
{
	class OperatorFactory
	{
		public IAsciiDubbleOperator GetOperator(char symbol, ArgumentsManager manager)
		{
			switch (symbol) {
				case '>':
					return new GreaterThenOperator(manager);
					break;
				case '<':
					return new SmallerThenOperator(manager);
					break;
				case '=':
					return new SameOperator(manager);
					break;
				case '&':
					return new AndOperator(manager);
					break;
				case '|':
					return new OrOperator(manager);
					break;

			}
			return null;
		}


	}
}
