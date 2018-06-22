using System.Linq;
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

        public override IAsciiBasePropositionalOperator Negate()
        {
            return ToAndOrNot().Negate();
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            var impl1 = new IfThenPropositionalOperator(_argumentManager);
            impl1.Instantiate(GetChilds().Select( x => x.ToAndOrNot()).ToArray());
            
            var impl2 = new IfThenPropositionalOperator(_argumentManager);
            impl2.Instantiate(GetChilds().Select( x => x.ToAndOrNot()).Reverse().ToArray());
            
            
            var and = new AndPropositionalOperator(_argumentManager);
            and.Instantiate(new []{impl1.ToAndOrNot(), impl2.ToAndOrNot()});

            return and;
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