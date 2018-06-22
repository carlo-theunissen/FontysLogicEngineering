using System.Linq;
using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    internal class OrPropositionalOperator : AbstractDubblePropositionalOperator
    {
        public OrPropositionalOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override bool Result()
        {
            return _A.Result() || _B.Result();
        }

        public override IAsciiBasePropositionalOperator ToNandify()
        {
            var nand = new NotAndPropositionalOperator(_argumentManager);
            var notA = new NotPropositionalOperator(_argumentManager);
            var notB = new NotPropositionalOperator(_argumentManager);
            notA.Instantiate(new []{ GetChilds()[0]});
            notB.Instantiate(new []{ GetChilds()[1]});
            nand.Instantiate(new [] {notA.ToNandify(), notB.ToNandify()});
            return nand;
        }

        public override IAsciiBasePropositionalOperator Negate()
        {
            var and = new AndPropositionalOperator(_argumentManager);
            and.Instantiate(new [] { SurroundWithNot(GetChilds()[0]), SurroundWithNot(GetChilds()[1])});
            return and;
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            var or = new OrPropositionalOperator(_argumentManager);
            or.Instantiate(GetChilds().Select( x => x.ToAndOrNot()).ToArray());
            return or;
        }

        public override char GetAsciiSymbol()
        {
            return '|';
        }

        public override char GetLogicSymbol()
        {
            return '∨';
        }
    }
}