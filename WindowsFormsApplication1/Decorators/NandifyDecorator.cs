using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Abstract;
using WindowsFormsApplication1.interfaces;
using WindowsFormsApplication1.Operators;

namespace WindowsFormsApplication1.Decorators
{
	class NandifyDecorator : AbstractParser
	{
		private ArgumentsManager _manager;
		private IAsciiBaseOperator _operator;
		public NandifyDecorator(IAsciiBaseOperator oper)
		{
			_manager = new ArgumentsManager();
			_operator = Process(oper);
		}
		public override IArgumentController GetArgumentController()
		{
			return _manager;
		}

		public override IAsciiBaseOperator GetOperator()
		{
			return _operator;
		}

		private IAsciiBaseOperator Process(IAsciiBaseOperator work)
		{
			IAsciiSingleOperator logic = work as IAsciiSingleOperator;
			if (logic != null)
			{
				switch (logic.GetSymbol()) {
					case '>':
						IAsciiBaseOperator not = NotToNand(logic.GetChilds()[0]);
						return OrToNand(not, logic.GetChilds()[1]);
						
					case '=':
						return SameToNand(logic.GetChilds()[0], logic.GetChilds()[1]);
					case '&':
						return AndToNand(logic.GetChilds()[0], logic.GetChilds()[1]);
					case '|':
						return OrToNand(logic.GetChilds()[0], logic.GetChilds()[1]);
					case '~':
						return NotToNand(logic.GetChilds()[0]);
					case '%':
						IAsciiBaseOperator nand = new NotAndOperator(_manager);
						nand.Instantiate(new IAsciiBaseOperator[] { Process(logic.GetChilds()[0]), Process(logic.GetChilds()[1]) });
						return nand;
				}
			}

			return work;

		}

		private IAsciiBaseOperator OrToNand(IAsciiBaseOperator oper1, IAsciiBaseOperator oper2)
		{
			IAsciiBaseOperator nand = new NotAndOperator(_manager);
			nand.Instantiate(new IAsciiBaseOperator[] { NotToNand(oper1), NotToNand(oper2) });
			return nand;
		}

		private IAsciiBaseOperator NotToNand(IAsciiBaseOperator oper1)
		{
			IAsciiBaseOperator nand = new NotAndOperator(_manager);
			nand.Instantiate(new IAsciiBaseOperator[] { Process(oper1), Process(oper1) });
			return nand;
		}
		private IAsciiBaseOperator SameToNand(IAsciiBaseOperator oper1, IAsciiBaseOperator oper2)
		{
			IAsciiBaseOperator notAnd = AndToNand(NotToNand(oper1), NotToNand(oper2));
			IAsciiBaseOperator and = AndToNand(oper1, oper2);
			return OrToNand(notAnd, and);

		}

		private IAsciiBaseOperator AndToNand(IAsciiBaseOperator oper1, IAsciiBaseOperator oper2){
			IAsciiBaseOperator nand = new NotAndOperator(_manager);
			nand.Instantiate(new IAsciiBaseOperator[] { Process(oper1), Process(oper2) });
			return NotToNand(nand);
		}
	}
}
