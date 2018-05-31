using Logic.interfaces;
using Logic.Operators;

namespace Logic.Abstract
{
    public abstract class AbstractConstantPropositionalOperator : AbstractBasePropositionalOperator, IAsciiSinglePropositionalOperator
    {

        public AbstractConstantPropositionalOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override void Instantiate(IAsciiBasePropositionalOperator[] arg)
        {
        }

        public override char[] GetArguments()
        {
            return new char[0];
        }

        public override IAsciiBasePropositionalOperator[] GetChilds()
        {
            return new IAsciiBasePropositionalOperator[0];
        }

        public override int GetOperatorNeededArguments()
        {
            return 0;
        }

        public abstract char GetAsciiSymbol();
        public abstract char GetLogicSymbol();

        public override string ToString()
        {
            return GetAsciiSymbol().ToString();
        }

        public override string ToLogicString()
        {
            return GetLogicSymbol().ToString();
        }
    }
}