using System;
using System.Collections.Generic;
using Logic.Decorators;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.SemanticTableaux
{
    public class OperatorSplitter
    {
        /*
        private ArgumentsManager _manager;

        public OperatorSplitter()
        {
            _manager = new ArgumentsManager();
        }
           
        
        public TableuaxStep[] SplitOperatorIntoSteps(IAsciiBasePropositionalOperator baseOperator)
        {
            var andOrNot = baseOperator.ToAndOrNot();
            if (andOrNot is AndPropositionalOperator and)
            {
                return SplitAndOperator(and);
            }
            
            if (andOrNot is OrPropositionalOperator or)
            {
                return SplitOrOperator(or);
            }
            
            if (andOrNot is NotPropositionalOperator not)
            {
                if (!(andOrNot.GetChilds()[0] is ScalarPropositionalOperator))
                {
                    return SplitOperatorIntoSteps(not.GetChilds()[0].ToDeMorgen());
                }
                return ScalarOperator(andOrNot.GetChilds()[0] as ScalarPropositionalOperator);
            }
            
            throw new NotImplementedException();
            return null;

        }

        /*
        private  TableuaxStep[] SplitNandOperator(NotAndPropositionalOperator nand)
        {
            var step1 = new TableuaxStep();
            var not1 = new NotPropositionalOperator(_manager);
            not1.Instantiate(new[] {nand.GetChilds()[0]});
            step1.AddOperator(not1);

            var step2 = new TableuaxStep();
            var not2 = new NotPropositionalOperator(_manager);
            not2.Instantiate(new[] {nand.GetChilds()[1]});
            step2.AddOperator(not2);
            return new[] {step1, step2};
        }

        private  TableuaxStep[] SplitIfThenOperator(IfThenPropositionalOperator implicatie)
        {
            var step1 = new TableuaxStep();
            var not = new NotPropositionalOperator( _manager );
            not.Instantiate(new[] {implicatie.GetChilds()[0]});
            step1.AddOperator(not);

            var step2 = new TableuaxStep();
            step2.AddOperator(implicatie.GetChilds()[1]);
            return new[] {step1, step2};
        }


        private TableuaxStep[] ScalarOperator(ScalarPropositionalOperator scalar)
        {
            var step1 = new TableuaxStep();
            step1.AddOperator(scalar);
            return new[] {step1};
        }
        private  TableuaxStep[] SplitOrOperator(OrPropositionalOperator or)
        {
            var step1 = new TableuaxStep();
            step1.AddOperator(or.GetChilds()[0]);

            var step2 = new TableuaxStep();
            step2.AddOperator(or.GetChilds()[1]);
            return new[] {step1, step2};
        }

        private static TableuaxStep[] SplitAndOperator(AndPropositionalOperator and)
        {
            var step = new TableuaxStep();
            step.AddOperator(and.GetChilds()[0]);
            step.AddOperator(and.GetChilds()[1]);
            return new[] {step};
        }
    */
        
    }
}