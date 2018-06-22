using System.Linq;
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
        public abstract void UpdateChild(int index, IAsciiBasePropositionalOperator baseOperator);
        public abstract string ToLogicString();
        public abstract string ToName();
        public abstract bool IsAdvanced();
        public abstract IAsciiBasePropositionalOperator ToNandify();
        public abstract IAsciiBasePropositionalOperator Negate();
        public abstract IAsciiBasePropositionalOperator ToAndOrNot();
        
        public virtual void ChangeArgument(char from, char to)
        {
            foreach (var asciiBasePropositionalOperator in GetChilds().Where(x =>
                !(x is ScalarPropositionalOperator) && x.GetChilds().Length > 0))
            {
                asciiBasePropositionalOperator.ChangeArgument(from, to);
            }

            var childs = GetChilds();
            for (var i = 0; i < childs.Length; i++)
            {
                if (!(childs[i] is ScalarPropositionalOperator t) || t.GetName() != @from) continue;
                
                UpdateChild(i, _argumentManager.RequestOperator(to));
                return;
            }
        }


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