using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Abstract;
using WindowsFormsApplication1.interfaces;

namespace WindowsFormsApplication1.Operators
{
	public class IfThenOperator : AbstractDubbleOperator
	{
		public IfThenOperator(ArgumentsManager manager) : base(manager)
		{
		}

		public override bool Result()
		{
			return !_A.Result() || _B.Result();
		}
		public override char GetSymbol()
		{
			return '>';
		}
	}
}
