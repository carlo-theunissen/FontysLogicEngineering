using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    internal class SamePropositionalOperator : AbstractDubblePropositionalOperator
    {
        public SamePropositionalOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override bool Result()
        {
            return _A.Result() == _B.Result();
        }

        public override IAsciiBasePropositionalOperator ToNandify()
        {
            var or = ToAndOrNot();            
            var resultOr = new OrPropositionalOperator(_argumentManager);
            resultOr.Instantiate(new []{or.GetChilds()[0].ToNandify(), or.GetChilds()[1].ToNandify() });
            return resultOr.ToNandify();
        }

        public override IAsciiBasePropositionalOperator ToDeMorgen()
        {
            return ToAndOrNot().ToDeMorgen();
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            var notA = new NotPropositionalOperator(_argumentManager);
            notA.Instantiate(new []{GetChilds()[0]});
            
            var notB = new NotPropositionalOperator(_argumentManager);
            notB.Instantiate(new []{GetChilds()[1]});
            
            var notAnd = new AndPropositionalOperator(_argumentManager);
            notAnd.Instantiate(new IAsciiBasePropositionalOperator[]{notA ,notB});
            
            var and = new AndPropositionalOperator(_argumentManager);
            and.Instantiate(GetChilds());
            
            var or = new OrPropositionalOperator(_argumentManager);
            or.Instantiate(new IAsciiBasePropositionalOperator[] {notAnd, and});
            return or;
        }

        public override char GetAsciiSymbol()
        {
            return '=';
        }

        public override char GetLogicSymbol()
        {
            return '↔';
        }
    }
}