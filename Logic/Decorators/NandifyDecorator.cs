using Logic.Abstract;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Decorators
{
    public class NandifyDecorator : AbstractParser
    {
        private readonly ArgumentsManager _manager;
        private readonly IAsciiBasePropositionalOperator _propositionalOperator;

        public NandifyDecorator(IAsciiBasePropositionalOperator oper)
        {
            _manager = new ArgumentsManager();
            _propositionalOperator = Process(oper);
        }

        public override IArgumentController GetArgumentController()
        {
            return _manager;
        }

        public override IAsciiBasePropositionalOperator GetOperator()
        {
            return _propositionalOperator;
        }

        private IAsciiBasePropositionalOperator Process(IAsciiBasePropositionalOperator work)
        {
            var logic = work as IAsciiSinglePropositionalOperator;
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
                        IAsciiBasePropositionalOperator nand = new NotAndPropositionalOperator(_manager);
                        nand.Instantiate(new[] {Process(logic.GetChilds()[0]), Process(logic.GetChilds()[1])});
                        return nand;
                    case 'F':
                        return new FalsePropositionalOperator(_manager);
                    case 'T':
                        return new TruePropositionalOperator(_manager);    
                    case '@':
                        var universal =  new UniversalQuantifierOperator(_manager);
                        if (work is UniversalQuantifierOperator workUnversal)
                        {
                            universal.SetVariable(workUnversal.GetVariable());
                            universal.Instantiate(new[] {Process(logic.GetChilds()[0])});
                            return universal;
                        }
                        break;
                    case '!':
                        var extensional =  new ExtensionalQuantifierOperator(_manager);
                        if (work is ExtensionalQuantifierOperator workExtensional)
                        {
                            extensional.SetVariable(workExtensional.GetVariable());
                            extensional.Instantiate(new[] {Process(logic.GetChilds()[0])});
                            return extensional;
                        }
                        break;
                        
                }

            if (work is PredicateOperator predicate)
            {
                var result = new PredicateOperator(_manager);
                result.SetName(predicate.GetName());
                foreach (var child in work.GetChilds())
                {
                    result.AddChild(Process(child));
                }
                return result;
            }
            
            var scalar = work as ScalarPropositionalOperator;
            if (scalar != null)
                return _manager.RequestOperator(scalar.GetName());
            throw new System.Exception("Operator not found");
        }

        private IAsciiBasePropositionalOperator OrToNand(IAsciiBasePropositionalOperator oper1, IAsciiBasePropositionalOperator oper2)
        {
            IAsciiBasePropositionalOperator nand = new NotAndPropositionalOperator(_manager);
            nand.Instantiate(new[] {NotToNand(oper1), NotToNand(oper2)});
            return nand;
        }

        private IAsciiBasePropositionalOperator NotToNand(IAsciiBasePropositionalOperator oper1)
        {
            IAsciiBasePropositionalOperator nand = new NotAndPropositionalOperator(_manager);
            nand.Instantiate(new[] {Process(oper1), Process(oper1)});
            return nand;
        }

        private IAsciiBasePropositionalOperator SameToNand(IAsciiBasePropositionalOperator oper1, IAsciiBasePropositionalOperator oper2)
        {
            var notAnd = AndToNand(NotToNand(oper1), NotToNand(oper2));
            var and = AndToNand(oper1, oper2);
            return OrToNand(notAnd, and);
        }

        private IAsciiBasePropositionalOperator AndToNand(IAsciiBasePropositionalOperator oper1, IAsciiBasePropositionalOperator oper2)
        {
            IAsciiBasePropositionalOperator nand = new NotAndPropositionalOperator(_manager);
            nand.Instantiate(new[] {Process(oper1), Process(oper2)});
            return NotToNand(nand);
        }
    }
}