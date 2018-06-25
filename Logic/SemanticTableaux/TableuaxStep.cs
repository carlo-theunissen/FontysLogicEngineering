using System;
using System.Collections;
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
        private IEnumerable<IAsciiBasePropositionalOperator> advancedOperatorsCached;
        private IEnumerator<IAsciiBasePropositionalOperator> operatorIEnumerable;
        private IList<char> ExtensionalVariables = new List<char>();
        
        private bool? cachedClosed;

        public IList<TableuaxStep> GetChilds()
        {
            return childs;
        }
        public IList<IAsciiBasePropositionalOperator> GetOperators()
        {
            return operators;
        }
        public TableuaxStep(IAsciiBasePropositionalOperator startOperator)
        {
            var propositional = startOperator;
            operators.Add(propositional);
            operatorIEnumerable = getNextParsingOperator();
        }

        private TableuaxStep(IList<IAsciiBasePropositionalOperator> otherOperators, IList<char> extensionalVariables)
        {
            this.ExtensionalVariables = extensionalVariables;
            operators = otherOperators;
            var t = operators.Aggregate("", (current, asciiBasePropositionalOperator) => current + (" , " + asciiBasePropositionalOperator.ToLogicString()));
            Console.WriteLine("NEW: "+ t);
            operatorIEnumerable = getNextParsingOperator();
        }

        private IEnumerable<IAsciiBasePropositionalOperator> getAdvancedOperators()
        {
            if (advancedOperatorsCached != null)
            {
                return advancedOperatorsCached;
            }
            return advancedOperatorsCached = operators.Where(x => x.IsAdvanced());
        }
        
        public bool IsClosed()
        {
            if (cachedClosed.HasValue && cachedClosed.Value)
            {
                return true;
            }
            var close = HasCounterOperators();
            var result = close || (childs.All( x => x.IsClosed()) && childs.Any());
            cachedClosed = result;
            if (result)
            {
                var t = operators.Aggregate("", (current, asciiBasePropositionalOperator) => current + (" , " + asciiBasePropositionalOperator.ToLogicString()));
                Console.WriteLine("CLOSED ONE!!" + this + t);
            }
            
            return result;
        }

        private bool HasCounterOperators()
        {
            var result =  operators.Any(x => operators.Any(y =>
            {
                if (x is FalsePropositionalOperator)
                {
                    return true;
                }
                
                var equals = !Equals(x, y);
                var yIsNot = (y is NotPropositionalOperator);
                var equals2 = false;
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
            var notclosed = !IsClosed();
            var any = getAdvancedOperators().Any(x => !operatorsMadeIntoChilds.Contains(x)) || childs.Any( x => !x.IsClosed());
            var next = (!childs.Any() || childs.All( x => x.HasNext() || x.IsClosed()));
            var result =  notclosed && next && any;
            if (!result)
            {
                Console.WriteLine("bla");
            }
            return result;
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
                return;
            }
            throw new NotImplementedException();
        }

        private void AddChild(IAsciiBasePropositionalOperator baseOperator)
        {
            
            
            //Console.WriteLine("Child: "+ baseOperator.ToLogicString());
           
            IAsciiBasePropositionalOperator work;
            if (baseOperator is NotPropositionalOperator)
            {
                var temp = baseOperator.GetChilds()[0].Negate();
                work = temp;
            }
            else
            {
                work = baseOperator;
            }

            var stop = operatorsMadeIntoChilds.Contains(work) || operatorsMadeIntoChilds.Contains(baseOperator);
            operatorsMadeIntoChilds.Add(baseOperator);
            
            if (stop)
            {
                Console.WriteLine("STOP!");
                return;
            }
            
            operatorsMadeIntoChilds.Add(work);
            
            
            if (work is OrPropositionalOperator)
            {

                AddOrChild(baseOperator, work.GetChilds()[0]);
                if (!work.GetChilds()[0].Equals(work.GetChilds()[1]))
                {
                    AddOrChild(baseOperator, work.GetChilds()[1]);
                }
                return;
            }
            if (work is AndPropositionalOperator)
            {
                List<IAsciiBasePropositionalOperator> list = operators.Where(x => x != baseOperator).ToList();
                var first = work.GetChilds()[0];
                var second = work.GetChilds()[1];
                if (!list.Contains(first))
                {
                    list.Add(first);
                }
                
                if (!list.Contains(second))
                {
                    list.Add(second);
                }
                
                var child = new TableuaxStep(list, ExtensionalVariables);
                
                childs.Add(child);
                return;
            }
            
            if (work is AbstractConstantPropositionalOperator)
            {
                List<IAsciiBasePropositionalOperator> list = operators.Where(x => x != baseOperator).ToList();
                if (!list.Contains(work))
                {
                    list.Add(work);
                }
                
                var child = new TableuaxStep(list, ExtensionalVariables); 
                childs.Add(child);
                return; 
            }
            if (work is AbstractQuantifierOperator)
            {
                HandleQuantifierOperators();
                return;
            }
            throw new NotImplementedException();
        }

        private void HandleQuantifierOperators()
        {
            var list = operators.Where(x => !(x is AbstractQuantifierOperator) &&  !(x is NotPropositionalOperator && x.GetChilds()[0] is AbstractQuantifierOperator)).ToList();
            foreach (var baseOperator in operators.Where(x => !list.Contains(x)).OrderBy(CalcScore))
            {
                IAsciiBasePropositionalOperator work;
                if (baseOperator is NotPropositionalOperator)
                {
                    var temp = baseOperator.GetChilds()[0].Negate();
                    work = temp;
                }
                else
                {
                    work = baseOperator;
                }
                
                operatorsMadeIntoChilds.Add(work);
                operatorsMadeIntoChilds.Add(baseOperator);
                
                if (!list.Contains(work))
                {
                    var q = work as AbstractQuantifierOperator;
                    if (work is UniversalQuantifierOperator)
                    {
                        list.Add(work);
                        foreach (var extensionalVariable in ExtensionalVariables)
                        {
                            var clone = StringParser.CloneOperator(work.GetChilds()[0]);
                            clone.ChangeArgument(q.GetVariable(), extensionalVariable);
                            if (!list.Contains(clone))
                            {
                                list.Add(clone);
                            }
                        }
                    }
                    else
                    {
                        
                        var clone = StringParser.CloneOperator(work.GetChilds()[0]);
                        var variable = clone.GetArgumentsManager().GetUnusedOperator();
                        ExtensionalVariables.Add(variable);
                        clone.ChangeArgument(q.GetVariable(), variable);
                        if (!list.Contains(clone))
                        {
                            list.Add(clone);
                        }
                    }
                }
            }
            
            var child = new TableuaxStep(list, ExtensionalVariables);
            childs.Add(child);
        }

        private bool AddOrChild(IAsciiBasePropositionalOperator baseOperator, IAsciiBasePropositionalOperator workChild)
        {
            List<IAsciiBasePropositionalOperator> list1 = operators.Where(x => x != baseOperator).ToList(); 
            if (!list1.Contains(workChild))
            {
                list1.Add(workChild);
            }
             
          
                
            var child1 = new TableuaxStep(list1, ExtensionalVariables);
            childs.Add(child1);
            return true;
        }
        
        public IEnumerator<IAsciiBasePropositionalOperator> getNextParsingOperator()
        {
            foreach (var baseOperator in getAdvancedOperators().OrderBy( CalcScore))
            {
                yield return baseOperator;
            }
        }

        private int CalcScore(IAsciiBasePropositionalOperator baseOperator)
        {
            switch (baseOperator)
            {
                case AndPropositionalOperator _:
                    return 1;
                case NotPropositionalOperator _:
                    return NotCalcScore(baseOperator.GetChilds()[0]);
                case OrPropositionalOperator _:
                    return 3;
                case ExtensionalQuantifierOperator _:
                    return 4;
                case UniversalQuantifierOperator _:
                    return 5;
                case AbstractConstantPropositionalOperator _:
                    return 6;
                    
            }
            throw new NotImplementedException();          
        }
        
        
        private int NotCalcScore(IAsciiBasePropositionalOperator baseOperator)
        {
            switch (baseOperator)
            {
                case AndPropositionalOperator _:
                    return 3;
                case NotPropositionalOperator _:
                    return CalcScore(baseOperator.GetChilds()[0]);
                case OrPropositionalOperator _:
                    return 1;
                case ExtensionalQuantifierOperator _:
                    return 5;
                case UniversalQuantifierOperator _:
                    return 4;
                case AbstractConstantPropositionalOperator _:
                    return 6;
            }
            throw new NotImplementedException();          
        }
    }
}