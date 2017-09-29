using Logic.Abstract;

namespace Logic.Operators
{
    internal class OrOperator : AbstractDubbleOperator
    {
        public OrOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override bool Result()
        {
            return _A.Result() || _B.Result();
        }

        public override char GetSymbol()
        {
            return '|';
        }
    }
}