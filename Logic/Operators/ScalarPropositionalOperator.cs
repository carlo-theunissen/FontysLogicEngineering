using Logic.Abstract;
using Logic.Exception;
using Logic.interfaces;

namespace Logic.Operators
{
    public class ScalarPropositionalOperator : AbstractBasePropositionalOperator
    {
        private readonly char _name;
        private bool? _result;

        public ScalarPropositionalOperator(char name, ArgumentsManager manager) : base(manager)
        {
            _name = name;
        }

        public void SetValue(bool? value)
        {
            _result = value;
        }

        public override IAsciiBasePropositionalOperator[] GetChilds()
        {
            return null;
        }

        public char GetName()
        {
            return _name;
        }

        public override bool HasResult()
        {
            return true;
        }

        public override bool Result()
        {
            if (!_result.HasValue)
                throw new ScalarInvalidValue();
            return _result.Value;
        }

        public override char[] GetArguments()
        {
            char[] temp = {_name};
            return temp;
        }

        public override int GetOperatorNeededArguments()
        {
            return 0;
        }

        public override void Instantiate(IAsciiBasePropositionalOperator[] arguments)
        {
        }

        public override string ToLogicString()
        {
            return ToString();
        }

        public override string ToString()
        {
            return _name.ToString();
        }
        
    }
}