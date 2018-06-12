using Logic.Abstract;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Decorators
{
    public class NotifyDecorator : AbstractParser
    {
        private readonly ArgumentsManager _manager;
        private readonly IAsciiBasePropositionalOperator _propositionalOperator;

        public NotifyDecorator(IAsciiBasePropositionalOperator oper)
        {
            _manager = new ArgumentsManager();
            _propositionalOperator = Process(oper);
        }
        
        public NotifyDecorator(IAsciiBasePropositionalOperator oper, ArgumentsManager manager)
        {
            _manager = manager;
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

        private IAsciiBasePropositionalOperator SurroundWithNot(IAsciiBasePropositionalOperator baseOperator)
        {
            if (baseOperator is NotPropositionalOperator baseNot)
            {
                return baseNot.GetChilds()[0];
            }
            var not = new NotPropositionalOperator(_manager);
            not.Instantiate(new []{baseOperator});
            return not;
        }
        
        private IAsciiBasePropositionalOperator Process(IAsciiBasePropositionalOperator work)
        {
            var logic = work as IAsciiSinglePropositionalOperator;
            if (logic != null)
                switch (logic.GetAsciiSymbol())
                {
                    case '>':
                        var or = new OrPropositionalOperator(_manager);
                        or.Instantiate(new [] { SurroundWithNot(logic.GetChilds()[0]), logic.GetChilds()[1]});
                        return HandleOr(or);                     
                    case '=':
                        var implication1 = new IfThenPropositionalOperator(_manager);
                        implication1.Instantiate(new []{ logic.GetChilds()[0],  logic.GetChilds()[1]});

                        var implication2 = new IfThenPropositionalOperator(_manager);
                        implication2.Instantiate(new IAsciiBasePropositionalOperator[]{  logic.GetChilds()[1],  logic.GetChilds()[0]});
                        
                        var and = new AndPropositionalOperator(_manager);
                        and.Instantiate(new IAsciiBasePropositionalOperator[]{ implication1, implication2});
                        return HandleAnd(and);
                    case '&':
                        return HandleAnd((AndPropositionalOperator) logic);
                    case '|':
                        return HandleOr((OrPropositionalOperator) logic);
                    case '~':
                        return logic;
                    case '%':
                        var nAnd = new AndPropositionalOperator(_manager);
                        nAnd.Instantiate(logic.GetChilds());
                        return SurroundWithNot(HandleAnd(nAnd));
                    case 'F':
                        return new TruePropositionalOperator(_manager);
                    case 'T':
                        return new FalsePropositionalOperator(_manager);    
                    case '@':
                        if (work is UniversalQuantifierOperator workUnversal)
                        {
                            var ext = new ExtensionalQuantifierOperator(_manager);
                            ext.SetVariable(workUnversal.GetVariable());
                            ext.Instantiate(new[] {SurroundWithNot(logic.GetChilds()[0])});
                            return SurroundWithNot(ext);
                        }
                        break;
                    case '!':
                        if (work is ExtensionalQuantifierOperator workExtensional)
                        {
                            var universal = new UniversalQuantifierOperator(_manager);
                            universal.SetVariable(workExtensional.GetVariable());
                            universal.Instantiate(new[] {SurroundWithNot(logic.GetChilds()[0])});
                            return SurroundWithNot(universal);
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

            if (work is ScalarPropositionalOperator scalar)
                return _manager.RequestOperator(scalar.GetName());
            throw new System.Exception("Operator not found");
        }

        private IAsciiBasePropositionalOperator HandleAnd(AndPropositionalOperator and)
        {
            var or = new OrPropositionalOperator(_manager);
            or.Instantiate(new []{SurroundWithNot( and.GetChilds()[0]), SurroundWithNot(and.GetChilds()[1])});
            return SurroundWithNot(or);
        }

        private IAsciiBasePropositionalOperator HandleOr(OrPropositionalOperator or)
        {
            var and = new AndPropositionalOperator(_manager);
            and.Instantiate(new []{SurroundWithNot( or.GetChilds()[0]), SurroundWithNot(or.GetChilds()[1])});
            return SurroundWithNot(and);           
        }
    }
}