using Logic.Abstract;

namespace Logic.Operators
{
    public class FalseOperator : AbstractConstantOperator
    {
        public FalseOperator(ArgumentsManager manager) : base(manager)
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

        public override bool Result()
        {
            return false;
        }

    }
}