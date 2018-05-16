using Logic.Abstract;

namespace Logic.Operators
{
    public class IfThenOperator : AbstractDubbleOperator
    {
        public IfThenOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override bool Result()
        {
            return !_A.Result() || _B.Result();
        }

        public override char GetAsciiSymbol()
        {
            return '>';
        }

        public override char GetLogicSymbol()
        {
            return '⇒';
        }
    }
}