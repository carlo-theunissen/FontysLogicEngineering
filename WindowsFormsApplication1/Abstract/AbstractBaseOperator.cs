using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.interfaces;
namespace WindowsFormsApplication1.Abstract
{
	public abstract class AbstractBaseOperator : IAsciiBaseOperator
	{
		protected ArgumentsManager _argumentManager;
		public ArgumentsManager GetArgumentsManager()
		{
			return _argumentManager;
		}
		public AbstractBaseOperator(ArgumentsManager manager)
		{
			_argumentManager = manager;
		}
		public abstract bool Result(); 

		public virtual bool Equals(IAsciiBaseOperator obj)
		{
			return Result().Equals(obj.Result());
		}

		abstract public IAsciiBaseOperator[] GetChilds();

		abstract public char[] GetArguments();

		abstract public int GetOperatorNeededArguments();
		public abstract void Instantiate(IAsciiBaseOperator[] arguments);
		
	}
}
