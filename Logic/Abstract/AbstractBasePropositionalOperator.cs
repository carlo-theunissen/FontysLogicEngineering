using Logic.interfaces;
using Logic.Operators;

namespace Logic.Abstract
{
    public abstract class AbstractBasePropositionalOperator : IAsciiBasePropositionalOperator
    {
        protected ArgumentsManager _argumentManager;

        public AbstractBasePropositionalOperator(ArgumentsManager manager)
        {
            _argumentManager = manager;
        }

        public ArgumentsManager GetArgumentsManager()
        {
            return _argumentManager;
        }

        public abstract bool HasResult();

        public abstract bool Result();

        public abstract IAsciiBasePropositionalOperator[] GetChilds();

        public abstract char[] GetArguments();

        public abstract int GetOperatorNeededArguments();
        public abstract void Instantiate(IAsciiBasePropositionalOperator[] arguments);
        public abstract string ToLogicString();
        public abstract bool IsAdvanced();
        public abstract IAsciiBasePropositionalOperator ToNandify();
        public abstract IAsciiBasePropositionalOperator ToDeMorgen();
        public abstract IAsciiBasePropositionalOperator ToAndOrNot();


        
        protected IAsciiBasePropositionalOperator SurroundWithNot(IAsciiBasePropositionalOperator baseOperator)
        {
            if (baseOperator is NotPropositionalOperator baseNot)
            {
                return baseNot.GetChilds()[0];
            }
            var not = new NotPropositionalOperator(_argumentManager);
            not.Instantiate(new []{baseOperator});
            return not;
        }

        public abstract override bool Equals(object obj);
    }
}