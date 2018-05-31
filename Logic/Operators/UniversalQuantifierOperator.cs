using Logic.Abstract;

namespace Logic.Operators
{
    public class UniversalQuantifierOperator : AbstractQuantifierOperator
    {
        public UniversalQuantifierOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override char GetAsciiSymbol()
        {
            return '@';
        }

        public override char GetLogicSymbol()
        {
            return '∀';
        }
    }
}