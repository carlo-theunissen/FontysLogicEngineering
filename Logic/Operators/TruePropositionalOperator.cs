using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class TruePropositionalOperator : AbstractConstantPropositionalOperator
    {
        public TruePropositionalOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override char GetAsciiSymbol()
        {
            return 'T';
        }

        public override char GetLogicSymbol()
        {
            return 'T';
        }

        public override bool HasResult()
        {
            return true;
        }

        public override bool Result()
        {
            return true;
        }

        public override IAsciiBasePropositionalOperator ToNandify()
        {
            return this;
        }

        public override IAsciiBasePropositionalOperator ToDeMorgen()
        {
            throw new System.NotImplementedException();
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            return this;
        }
    }
}