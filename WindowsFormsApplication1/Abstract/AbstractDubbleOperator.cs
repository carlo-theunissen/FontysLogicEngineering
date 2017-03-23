using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.interfaces;
namespace WindowsFormsApplication1.Abstract
{
	public abstract class AbstractDubbleOperator : AbstractSingleOperator, IAsciiDubbleOperator
	{
		

		protected IAsciiBaseOperator _A;
		protected IAsciiBaseOperator _B;

		public AbstractDubbleOperator(ArgumentsManager manager) : base(manager)
		{
		}

		public void Instantiate(IAsciiBaseOperator a, IAsciiBaseOperator b)
		{
			_A = a;
			_B = b;
		}

		public override IAsciiBaseOperator[] GetChilds()
		{
			IAsciiBaseOperator[] array = { _A, _B };
			return array;
		}
		public override char[] GetArguments()
		{
			HashSet<char> list = new HashSet<char>(_A.GetArguments());
			list.UnionWith(_B.GetArguments());
			return list.ToArray();
		}

	}
}
