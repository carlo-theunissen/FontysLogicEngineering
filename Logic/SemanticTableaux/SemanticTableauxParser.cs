using System.Collections.Generic;
using System.Linq;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.SemanticTableaux
{
    public class SemanticTableauxParser
    {
        private TableuaxStep Step;

        public SemanticTableauxParser(IAsciiBasePropositionalOperator baseOperator)
        {
            var not = new NotPropositionalOperator(baseOperator.GetArgumentsManager());
            not.Instantiate(new []{baseOperator});
            Step = new TableuaxStep(not);
            HandleSteps();
            
        }

        private void HandleSteps()
        {
            while (Step.HasNext() && !Step.IsClosed())
            {
                Step.Step();
            }
        }

        public bool IsTautology()
        {
            return Step.IsClosed();
        }

        public TableuaxStep GetStep()
        {
            return Step;
        }
        
        
        
    }
    
}