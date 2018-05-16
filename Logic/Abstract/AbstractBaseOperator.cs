using Logic.interfaces;

namespace Logic.Abstract
{
    public abstract class AbstractBaseOperator : IAsciiBaseOperator
    {
        protected ArgumentsManager _argumentManager;

        public AbstractBaseOperator(ArgumentsManager manager)
        {
            _argumentManager = manager;
        }

        public ArgumentsManager GetArgumentsManager()
        {
            return _argumentManager;
        }

        public abstract bool Result();

        public abstract IAsciiBaseOperator[] GetChilds();

        public abstract char[] GetArguments();

        public abstract int GetOperatorNeededArguments();
        public abstract void Instantiate(IAsciiBaseOperator[] arguments);
        public abstract string ToLogicString();
        

        public virtual bool Equals(IAsciiBaseOperator obj)
        {
            return Result().Equals(obj.Result());
        }
    }
}