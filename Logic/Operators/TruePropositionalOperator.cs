using Logic.Abstract;

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

    }
}