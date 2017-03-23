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

		public virtual void Instantiate(IAsciiBaseOperator a)
		{
			_A = a;
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
	}
}
