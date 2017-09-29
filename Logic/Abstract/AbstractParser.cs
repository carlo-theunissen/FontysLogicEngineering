using Logic.interfaces;

namespace Logic.Abstract
{
    public abstract class AbstractParser : IParser
    {
        protected static readonly OperatorFactory _operatorFactory;

        /**
         * Static constructor
         */
        static AbstractParser()
        {
            _operatorFactory = new OperatorFactory();
        }

        public abstract IArgumentController GetArgumentController();
        public abstract IAsciiBaseOperator GetOperator();
    }
}