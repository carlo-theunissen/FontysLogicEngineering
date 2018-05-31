using Logic.Abstract;

namespace Logic.Operators
{
    public class ExtensionalQuantifierOperator : AbstractQuantifierOperator
    {
        public ExtensionalQuantifierOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override char GetAsciiSymbol()
        {
            return '!';
        }

        public override char GetLogicSymbol()
        {
            return '∃';
        }
    }
}