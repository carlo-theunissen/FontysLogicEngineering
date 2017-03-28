using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.interfaces;

namespace WindowsFormsApplication1.Abstract
{
	public abstract class AbstractParser : IParser
	{
		protected readonly static OperatorFactory _operatorFactory;

		/**
		 * Static constructor
		 */
		static AbstractParser()
		{
			_operatorFactory = new OperatorFactory();
		}

		public abstract IArgumentController GetArgumentController();
		public abstract IAsciiBaseOperator GetOperator();
	}
}
