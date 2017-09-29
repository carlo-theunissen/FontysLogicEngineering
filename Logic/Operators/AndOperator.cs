using Logic.Abstract;

namespace Logic.Operators
{
    public class AndOperator : AbstractDubbleOperator
    {
        public AndOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override char GetSymbol()
        {
            return '&';
        }

        public override bool Result()
        {
            return _A.Result() && _B.Result();
        }
    }
}