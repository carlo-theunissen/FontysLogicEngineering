using System.Linq;
using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class AndPropositionalOperator : AbstractDubblePropositionalOperator
    {
        public AndPropositionalOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override char GetAsciiSymbol()
        {
            return '&';
        }

        public override char GetLogicSymbol()
        {
            return '∧';
        }

        public override bool Result()
        {
            return _A.Result() && _B.Result();
        }

        public override IAsciiBasePropositionalOperator ToNandify()
        {
            var not = new NotPropositionalOperator(_argumentManager);
            var nand = new NotAndPropositionalOperator(_argumentManager);
            nand.Instantiate(new[] {GetChilds()[0].ToNandify(), GetChilds()[1].ToNandify()});
            not.Instantiate(new IAsciiBasePropositionalOperator[] {nand});
            return not.ToNandify();
        }

        public override IAsciiBasePropositionalOperator Negate()
        {
            var or = new OrPropositionalOperator(_argumentManager);
            or.Instantiate(new []{SurroundWithNot( GetChilds()[0]), SurroundWithNot(GetChilds()[1])});
            return or;
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            var and = new AndPropositionalOperator(_argumentManager);
            and.Instantiate(GetChilds().Select( x => x.ToAndOrNot()).ToArray());
            return and;
        }
    }
}