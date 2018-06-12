using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Abstract;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.SemanticTableaux
{
    public class TableuaxStep
    {
        private IList<TableuaxStep> childs = new List<TableuaxStep>();
        private IList<IAsciiBasePropositionalOperator> operators = new List<IAsciiBasePropositionalOperator>();
        private IList<IAsciiBasePropositionalOperator> operatorsMadeIntoChilds = new List<IAsciiBasePropositionalOperator>();
        private IEnumerator<IAsciiBasePropositionalOperator> operatorIEnumerable;
        private bool? cachedClosed;
        public TableuaxStep(IAsciiBasePropositionalOperator startOperator)
        {
            var propositional = startOperator.ToAndOrNot(); //first make sure every operator in the Tableuax is in not/and/or format
            operators.Add(propositional);
            operatorIEnumerable = getNextParsingOperator();
        }

        private TableuaxStep(IList<IAsciiBasePropositionalOperator> otherOperators)
        {
            operators = otherOperators;
            operatorIEnumerable = getNextParsingOperator();
        }

        private IList<IAsciiBasePropositionalOperator> getAdvancedOperators()
        {
            return operators.Where(x => x.IsAdvanced()).Select(x => x.ToAndOrNot()).ToList();
        }
        
        public bool IsClosed()
        {
            var close = HasCounterOperators();
            var result =  close || (childs.All( x => x.IsClosed()) && childs.Any());
            cachedClosed = result;
            return result;
        }

        private bool HasCounterOperators()
        {
            var result =  operators.Any(x => operators.Any(y =>
            {
                var equals = !Equals(x, y);
                var yIsNot = (y is NotPropositionalOperator);
                bool equals2 = false;
                if (yIsNot)
                {
                    equals2 = y.GetChilds()[0].Equals(x);
                }
                return equals && yIsNot && equals2;
            }));
            return result;
        }

        public bool HasNext()
        {
            var next = !HasCounterOperators() && getAdvancedOperators().Any() && ( getAdvancedOperators().Any(x => !operatorsMadeIntoChilds.Contains(x)) || childs.Any(x => x.HasNext()));
            return next;
        }
        
        public void Step()
        {
            foreach (var tableuaxStep in childs)
            {
                if (tableuaxStep.HasNext() && !tableuaxStep.IsClosed())
                {
                    tableuaxStep.Step();
                    return;
                }
            }
            if (!IsClosed() && operatorIEnumerable.MoveNext())
            {
                var work = operatorIEnumerable.Current;
                AddChild(work);
            }
        }

        private void AddChild(IAsciiBasePropositionalOperator baseOperator)
        {
            operatorsMadeIntoChilds.Add(baseOperator);
            IAsciiBasePropositionalOperator work;
            if (baseOperator is NotPropositionalOperator)
            {
                var temp = baseOperator.GetChilds()[0].ToDeMorgen();
                work = temp;
            }
            else
            {
                work = baseOperator;
            }
            
            
            if (work is OrPropositionalOperator)
            {
                var list1 = operators.Where(x => x != baseOperator).ToList();
                var list2 = operators.Where(x => x != baseOperator).ToList();
                
                list1.Add(work.GetChilds()[0].ToAndOrNot());
                list2.Add(work.GetChilds()[1].ToAndOrNot());
                
                var child1 = new TableuaxStep(list1);
                var child2 = new TableuaxStep(list2);
                
                childs.Add(child1);
                childs.Add(child2);
                return;
            }
            if (work is AndPropositionalOperator)
            {
                var list = operators.Where(x => x != baseOperator).ToList();
                
                list.Add(work.GetChilds()[0].ToAndOrNot());
                list.Add(work.GetChilds()[1].ToAndOrNot());
                
                var child = new TableuaxStep(list);
                
                childs.Add(child);
                return;
            }
            throw new NotImplementedException();
        }
        
        public IEnumerator<IAsciiBasePropositionalOperator> getNextParsingOperator()
        {
            foreach (var baseOperator in getAdvancedOperators().OrderByDescending( x => CalcScore(x)))
            {
                yield return baseOperator;
            }
        }

        private int CalcScore(IAsciiBasePropositionalOperator baseOperator)
        {
            switch (baseOperator)
            {
                case AndPropositionalOperator _:
                    return 10;
                case NotPropositionalOperator _:
                    switch (baseOperator.GetChilds()[0])
                    {
                        case OrPropositionalOperator _:
                            return 10;
                        case AndPropositionalOperator _:
                            return 1;
                    }
                    return 5;
                case OrPropositionalOperator _:
                    return 1;
            }
            return 5;
        }
    }
}