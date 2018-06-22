using System.Linq;
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

        public override bool IsAdvanced()
        {
            return GetChilds().Any(x => x.IsAdvanced());
        }

        public override IAsciiBasePropositionalOperator ToNandify()
        {
            var extensional = new ExtensionalQuantifierOperator(_argumentManager);
            extensional.SetVariable(GetVariable());
            extensional.Instantiate(new []{GetChilds()[0].ToNandify()});
            return extensional;
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
            var extensional = new ExtensionalQuantifierOperator(_argumentManager);
            extensional.SetVariable(GetVariable());
            extensional.Instantiate(new []{GetChilds()[0].ToAndOrNot()});
            return extensional;
        }
    }
}