using Logic.Abstract;

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