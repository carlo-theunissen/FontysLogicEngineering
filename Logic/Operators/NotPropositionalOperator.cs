using Logic.Abstract;

namespace Logic.Operators
{
    internal class NotPropositionalOperator : AbstractSinglePropositionalOperator
    {
        public NotPropositionalOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override bool Result()
        {
            return !_A.Result();
        }

        public override char GetAsciiSymbol()
        {
            return '~';
        }

        public override char GetLogicSymbol()
        {
            return '¬';
        }
    }
}