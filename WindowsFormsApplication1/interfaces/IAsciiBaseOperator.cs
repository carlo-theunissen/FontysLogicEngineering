using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.interfaces
{
	public interface IAsciiBaseOperator
	{
		char[] GetArguments();
		bool Result();
		IAsciiBaseOperator[] GetChilds();

		/**
		 * Every Operator needs the ability to set the arguments.
		 * This way a user can simply call this function and modify the result. 
		 */ 
		ArgumentsManager GetArgumentsManager();

		int GetOperatorNeededArguments();

		void Instantiate(IAsciiBaseOperator[] arguments);
	}
}
