using Logic.Abstract;

namespace Logic.Operators
{
    public class TrueOperator : AbstractConstantOperator
    {
        public TrueOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override char GetAsciiSymbol()
        {
            return 'T';
        }

        public override char GetLogicSymbol()
        {
            return 'T';
        }

        public override bool Result()
        {
            return true;
        }

    }
}