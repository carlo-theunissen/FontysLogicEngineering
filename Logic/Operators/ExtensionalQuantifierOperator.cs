using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class ExtensionalQuantifierOperator : AbstractQuantifierOperator
    {
        public ExtensionalQuantifierOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override char GetAsciiSymbol()
        {
            return '!';
        }

        public override char GetLogicSymbol()
        {
            return '∃';
        }
        public override IAsciiBasePropositionalOperator ToNandify()
        {
            var extensional = new ExtensionalQuantifierOperator(_argumentManager);
            extensional.SetVariable(GetVariable());
            extensional.Instantiate(new []{GetChilds()[0].ToNandify()});
            return extensional;
        }

        public override IAsciiBasePropositionalOperator ToDeMorgen()
        {
            throw new System.NotImplementedException();
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            throw new System.NotImplementedException();
        }
        public override bool IsAdvanced()
        {
            throw new System.NotImplementedException();
        }
    }
}