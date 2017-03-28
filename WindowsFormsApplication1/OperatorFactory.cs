using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.interfaces;
using WindowsFormsApplication1.Operators;
namespace WindowsFormsApplication1
{
	public class OperatorFactory
	{
		public IAsciiBaseOperator GetOperator(char symbol, ArgumentsManager manager)
		{
			switch (symbol) {
				case '>':
					return new IfThenOperator(manager);
				case '=':
					return new SameOperator(manager);
				case '&':
					return new AndOperator(manager);
				case '|':
					return new OrOperator(manager);
				case '~':
					return new NotOperator(manager);
				case '%':
					return new NotAndOperator(manager);

			}
			return null;
		}


	}
}
