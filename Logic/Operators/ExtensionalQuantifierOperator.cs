using System.Collections.Generic;
using System.Linq;
using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class ExtensionalQuantifierOperator : AbstractQuantifierOperator
    {
        private IList<char> ExtensionalVariables = new List<char>();

        public IList<char> GetExtensionalVariables()
        {
            return ExtensionalVariables;
        }

        public void AddExtensionalVariable(char variable)
        {
            ExtensionalVariables.Add(variable);
        }
        
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
            return true;
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
            var universal = new UniversalQuantifierOperator(_argumentManager);
            universal.SetVariable(GetVariable());
            universal.Instantiate(new []{GetChilds()[0].Negate()});
            return universal;
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