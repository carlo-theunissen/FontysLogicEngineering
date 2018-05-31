using Logic.Abstract;

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

    }
}