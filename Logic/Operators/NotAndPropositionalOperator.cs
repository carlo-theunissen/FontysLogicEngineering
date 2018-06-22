using System.Linq;
using Logic.Abstract;
using Logic.Decorators;
using Logic.interfaces;

namespace Logic.Operators
{
    internal class NotAndPropositionalOperator : AbstractDubblePropositionalOperator
    {
        public NotAndPropositionalOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override char GetAsciiSymbol()
        {
            return '%';
        }

        public override char GetLogicSymbol()
        {
            return '%';
        }

        public override bool Result()
        {
            return !(_A.Result() && _B.Result());
        }

        public override IAsciiBasePropositionalOperator ToNandify()
        {
            var nand = new NotAndPropositionalOperator(_argumentManager);
            nand.Instantiate(new []{ GetChilds()[0].ToNandify(), GetChilds()[1].ToNandify()});
            return nand;
        }

        public override IAsciiBasePropositionalOperator Negate()
        {
            var and = new AndPropositionalOperator(_argumentManager);
            and.Instantiate(GetChilds());
            return and;
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            var not = new NotPropositionalOperator(_argumentManager);
            var and = new AndPropositionalOperator(_argumentManager);
            and.Instantiate(GetChilds().Select(x => x.ToAndOrNot()).ToArray());
            not.Instantiate(new IAsciiBasePropositionalOperator[]{and});
            return not;
        }
    }
}