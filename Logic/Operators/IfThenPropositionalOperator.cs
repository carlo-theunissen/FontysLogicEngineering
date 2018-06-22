using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class IfThenPropositionalOperator : AbstractDubblePropositionalOperator
    {
        public IfThenPropositionalOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override bool Result()
        {
            return !_A.Result() || _B.Result();
        }

        public override IAsciiBasePropositionalOperator ToNandify()
        {
            var nand = new NotAndPropositionalOperator(_argumentManager);
            var not = new NotPropositionalOperator(_argumentManager);
            not.Instantiate(new [] {GetChilds()[1]});
            nand.Instantiate(new[] {GetChilds()[0].ToNandify(), not.ToNandify()});
            return nand;
        }

        public override IAsciiBasePropositionalOperator Negate()
        {
            return ToAndOrNot().Negate();
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            var or = new OrPropositionalOperator(_argumentManager);
            or.Instantiate(new [] { SurroundWithNot(GetChilds()[0].ToAndOrNot()), GetChilds()[1].ToAndOrNot()});
            return or;
        }

        public override char GetAsciiSymbol()
        {
            return '>';
        }

        public override char GetLogicSymbol()
        {
            return '⇒';
        }
    }
}