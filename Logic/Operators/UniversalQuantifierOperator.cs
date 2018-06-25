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

        public override IAsciiBasePropositionalOperator Negate()
        {
            var extensional = new ExtensionalQuantifierOperator(_argumentManager);
            extensional.SetVariable(GetVariable());
            extensional.Instantiate(new []{GetChilds()[0].Negate()});
            return extensional;
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            var universal = new UniversalQuantifierOperator(_argumentManager);
            universal.SetVariable(GetVariable());
            universal.Instantiate(new []{GetChilds()[0].ToAndOrNot()});
            return universal;
        }
        public override bool IsAdvanced()
        {
            return true;
        }
    }
}