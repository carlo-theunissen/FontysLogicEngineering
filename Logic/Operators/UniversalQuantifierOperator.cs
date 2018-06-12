using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class UniversalQuantifierOperator : AbstractQuantifierOperator
    {
        public UniversalQuantifierOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override char GetAsciiSymbol()
        {
            return '@';
        }

        public override char GetLogicSymbol()
        {
            return '∀';
        }

        public override IAsciiBasePropositionalOperator ToNandify()
        {
            var universal = new UniversalQuantifierOperator(_argumentManager);
            universal.SetVariable(GetVariable());
            universal.Instantiate(new []{GetChilds()[0].ToNandify()});
            return universal;
        }

        public override IAsciiBasePropositionalOperator ToDeMorgen()
        {
            throw new System.NotImplementedException();
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            return this;
        }
        public override bool IsAdvanced()
        {
            throw new System.NotImplementedException();
        }
    }
}