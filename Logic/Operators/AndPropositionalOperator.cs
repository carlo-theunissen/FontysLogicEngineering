using Logic.Abstract;

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
    }
}