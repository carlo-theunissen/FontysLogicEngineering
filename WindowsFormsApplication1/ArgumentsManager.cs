using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Operators;

namespace WindowsFormsApplication1
{
	public class ArgumentsManager
	{
		private Dictionary<char, ScalarOperator> _requestedArguments;

		public ArgumentsManager()
		{
			_requestedArguments = new Dictionary<char, ScalarOperator>();
		}

		public ScalarOperator RequestOperator(char name)
		{
			if (_requestedArguments.Keys.Contains(name))
			{
				return _requestedArguments[name];
			}
			_requestedArguments[name] = new ScalarOperator(name, this);
			return _requestedArguments[name];
		}

		public bool SetArgumentValue(char name, bool? value)
		{
			if (_requestedArguments.Keys.Contains(name))
			{
				_requestedArguments[name].SetValue(value);
				return true;
			}
			return false;
		}

	}
}
