using System.Collections.Generic;
using System.Linq;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Abstract
{
    public abstract class AbstractDubbleOperator : AbstractSingleOperator, IAsciiDubbleOperator
    {
        protected IAsciiBaseOperator _B;

        public AbstractDubbleOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override void Instantiate(IAsciiBaseOperator[] arg)
        {
            _A = arg[0];
            _B = arg[1];
        }

        public override IAsciiBaseOperator[] GetChilds()
        {
            IAsciiBaseOperator[] array = {_A, _B};
            return array;
        }

        public override char[] GetArguments()
        {
            var list = new HashSet<char>(_A.GetArguments());
            list.UnionWith(_B.GetArguments());
            return list.OrderBy(x=> x).ToArray();
        }

        public override int GetOperatorNeededArguments()
        {
            return 2;
        }

        public override string ToString()
        {
            return string.Format("{0}( {1}, {2} )", GetAsciiSymbol(), _A, _B);
        }

        public override string ToLogicString()
        {
            var a = _A is ScalarOperator || !(_A is IAsciiDubbleOperator) ? _A.ToLogicString() : "(" + _A.ToLogicString() + ")";
            var b = _B is ScalarOperator || !(_B is IAsciiDubbleOperator) ? _B.ToLogicString() : "(" + _B.ToLogicString() + ")";
            return string.Format("{0} {1} {2}", a, GetLogicSymbol(), b);
        }
    }
}