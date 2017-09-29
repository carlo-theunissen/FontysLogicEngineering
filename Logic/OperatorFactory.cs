using Logic.interfaces;
using Logic.Operators;

namespace Logic
{
    public class OperatorFactory
    {
        public IAsciiBaseOperator GetOperator(char symbol, ArgumentsManager manager)
        {
            switch (symbol)
            {
                case '>':
                    return new IfThenOperator(manager);
                case '=':
                    return new SameOperator(manager);
                case '&':
                    return new AndOperator(manager);
                case '|':
                    return new OrOperator(manager);
                case '~':
                    return new NotOperator(manager);
                case '%':
                    return new NotAndOperator(manager);
            }
            return null;
        }
    }
}