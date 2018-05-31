using Logic.Abstract;

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