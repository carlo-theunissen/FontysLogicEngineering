using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class FalsePropositionalOperator : AbstractConstantPropositionalOperator
    {
        public FalsePropositionalOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override char GetAsciiSymbol()
        {
            return 'F';
        }

        public override char GetLogicSymbol()
        {
            return 'F';
        }

        public override bool HasResult()
        {
            return true;
        }

        public override bool Result()
        {
            return false;
        }

        public override IAsciiBasePropositionalOperator ToNandify()
        {
            return this;
        }

        public override IAsciiBasePropositionalOperator Negate()
        {
            return new TruePropositionalOperator(_argumentManager);
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            return this;
        }
        public override string ToName()
        {
            return "False";
        }
    }
}