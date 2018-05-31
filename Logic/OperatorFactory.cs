using Logic.interfaces;
using Logic.Operators;

namespace Logic
{
    public class OperatorFactory
    {
        public IAsciiBasePropositionalOperator GetOperator(char symbol, ArgumentsManager manager)
        {
            switch (symbol)
            {
                case '>':
                    return new IfThenPropositionalOperator(manager);
                case '=':
                    return new SamePropositionalOperator(manager);
                case '&':
                    return new AndPropositionalOperator(manager);
                case '|':
                    return new OrPropositionalOperator(manager);
                case '~':
                    return new NotPropositionalOperator(manager);
                case '%':
                    return new NotAndPropositionalOperator(manager);
                case 'F':
                    return new FalsePropositionalOperator(manager);
                case 'T':
                    return new TruePropositionalOperator(manager);
                case '@':
                    return new UniversalQuantifierOperator(manager);
                case '!':
                    return new ExtensionalQuantifierOperator(manager);
            }
            return null;
        }
    }
}