using System.Collections.Generic;
using System.Linq;
using Logic.interfaces;
using Logic.Operators;

namespace Logic
{
    public class ArgumentsManager : IArgumentController
    {
        private readonly Dictionary<char, ScalarOperator> _requestedArguments;

        public ArgumentsManager()
        {
            _requestedArguments = new Dictionary<char, ScalarOperator>();
        }

        public bool SetArgumentValue(char name, bool? value)
        {
            if (_requestedArguments.Keys.Contains(name))
            {
                _requestedArguments[name].SetValue(value);
                return true;
            }
            return false;
        }

        public ScalarOperator RequestOperator(char name)
        {
            if (_requestedArguments.Keys.Contains(name))
                return _requestedArguments[name];
            _requestedArguments[name] = new ScalarOperator(name, this);
            return _requestedArguments[name];
        }
    }
}