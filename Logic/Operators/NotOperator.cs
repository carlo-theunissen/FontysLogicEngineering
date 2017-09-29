using Logic.Abstract;

namespace Logic.Operators
{
    internal class NotOperator : AbstractSingleOperator
    {
        public NotOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override bool Result()
        {
            return !_A.Result();
        }

        public override char GetSymbol()
        {
            return '~';
        }
    }
}