using Logic.interfaces;
using Logic.Operators;

namespace Logic.Abstract
{
    public abstract class AbstractQuantifierOperator : AbstractSinglePropositionalOperator
    {
        private char Variable;
        protected AbstractQuantifierOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override bool HasResult()
        {
            return false;
        }

        public override bool Result()
        {
            throw new System.NotImplementedException(); //an quantifier doesn't have a result 
        }

        public void SetVariable(char variable)
        {
            Variable = variable;
        }

        public char GetVariable()
        {
            return Variable;
        }

        public void ChangeVariable(char newVariable)
        {
            _A.ChangeArgument(Variable, newVariable);
            SetVariable(newVariable);
        }
        public override string ToString()
        {
            return string.Format("{0}{1}.({2})", GetAsciiSymbol(), Variable , _A);
        }

        public override string ToLogicString()
        {
          //  var a = _A is ScalarPropositionalOperator || !(_A is IAsciiDubblePropositionalOperator)  ? _A.ToLogicString() : "(" + _A.ToLogicString() + ")";
            var result =  string.Format("{0}{1}.({2})", GetLogicSymbol(), Variable , _A.ToLogicString());
            return result;
        }
    }
}