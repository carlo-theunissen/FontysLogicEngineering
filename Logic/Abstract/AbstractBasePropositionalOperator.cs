using Logic.interfaces;

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
        

        public virtual bool Equals(IAsciiBasePropositionalOperator obj)
        {
            return Result().Equals(obj.Result());
        }
    }
}