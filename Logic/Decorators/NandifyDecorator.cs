using Logic.Abstract;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Decorators
{
    public class NandifyDecorator : AbstractParser
    {
        private readonly ArgumentsManager _manager;
        private readonly IAsciiBaseOperator _operator;

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
            var logic = work as IAsciiSingleOperator;
            if (logic != null)
                switch (logic.GetAsciiSymbol())
                {
                    case '>':
                        var not = NotToNand(logic.GetChilds()[0]);
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
                        nand.Instantiate(new[] {Process(logic.GetChilds()[0]), Process(logic.GetChilds()[1])});
                        return nand;
                    case 'F':
                        return new FalseOperator(_manager);
                    case 'T':
                        return new TrueOperator(_manager);                       
                        
                }

            var scalar = work as ScalarOperator;
            if (scalar != null)
                return _manager.RequestOperator(scalar.GetName());
            throw new System.Exception("Operator not found");
        }

        private IAsciiBaseOperator OrToNand(IAsciiBaseOperator oper1, IAsciiBaseOperator oper2)
        {
            IAsciiBaseOperator nand = new NotAndOperator(_manager);
            nand.Instantiate(new[] {NotToNand(oper1), NotToNand(oper2)});
            return nand;
        }

        private IAsciiBaseOperator NotToNand(IAsciiBaseOperator oper1)
        {
            IAsciiBaseOperator nand = new NotAndOperator(_manager);
            nand.Instantiate(new[] {Process(oper1), Process(oper1)});
            return nand;
        }

        private IAsciiBaseOperator SameToNand(IAsciiBaseOperator oper1, IAsciiBaseOperator oper2)
        {
            var notAnd = AndToNand(NotToNand(oper1), NotToNand(oper2));
            var and = AndToNand(oper1, oper2);
            return OrToNand(notAnd, and);
        }

        private IAsciiBaseOperator AndToNand(IAsciiBaseOperator oper1, IAsciiBaseOperator oper2)
        {
            IAsciiBaseOperator nand = new NotAndOperator(_manager);
            nand.Instantiate(new[] {Process(oper1), Process(oper2)});
            return NotToNand(nand);
        }
    }
}