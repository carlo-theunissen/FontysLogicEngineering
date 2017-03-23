using WindowsFormsApplication1.Exception;
using WindowsFormsApplication1.interfaces;
using WindowsFormsApplication1.Abstract;
using System;

namespace WindowsFormsApplication1.Operators
{
	public class ScalarOperator : AbstractBaseOperator
	{
		private bool? _result;
		private char _name;
		public ScalarOperator(char name, ArgumentsManager manager): base(manager)
		{
			_name = name;
		}
		public void SetValue(bool? value)
		{
			_result = value;
		}
		public override IAsciiBaseOperator[] GetChilds()
		{
			return null;
		}

		public override bool Result()
		{
			if (!_result.HasValue)
			{
				throw new ScalarInvalidValue();
			}
			return _result.Value;
		}
		public override char[] GetArguments()
		{
			char[] temp = { _name };
			return temp;
		}

		public override int GetOperatorNeededArguments()
		{
			return 0;
		}

		public override void Instantiate(IAsciiBaseOperator[] arguments){}
	}
}
