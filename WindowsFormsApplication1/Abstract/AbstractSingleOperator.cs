using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.interfaces;
namespace WindowsFormsApplication1.Abstract
{
	public abstract class AbstractSingleOperator : AbstractBaseOperator, IAsciiSingleOperator
	{ 
		

		protected IAsciiBaseOperator _A;

		public AbstractSingleOperator(ArgumentsManager manager) : base(manager)
		{
		}

		public override void Instantiate(IAsciiBaseOperator[] arg)
		{
			_A = arg[0];
		}
		public override char[] GetArguments()
		{
			return _A.GetArguments();
		}
		public override IAsciiBaseOperator[] GetChilds()
		{
			IAsciiBaseOperator[] array = { _A };
			return array;
		}
		public override int GetOperatorNeededArguments()
		{
			return 1;
		}
		public override string ToString()
		{
			return String.Format("{0}({1})", GetSymbol(), _A);
		}
		public abstract char GetSymbol();
	}
}
