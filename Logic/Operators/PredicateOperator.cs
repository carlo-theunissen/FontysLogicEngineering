using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class PredicateOperator : AbstractBasePropositionalOperator
    {
        private char Name;
        private readonly List<IAsciiBasePropositionalOperator> Operators = new List<IAsciiBasePropositionalOperator>();
            
        public PredicateOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public void SetName(char name)
        {
            Name = name;
        }

        public void AddChild(IAsciiBasePropositionalOperator baseOperator)
        {
            Operators.Add(baseOperator);
        }
        
        public override bool HasResult()
        {
            return false;
        }

        public override bool Result()
        {
            throw new NotImplementedException();
        }

        public override char[] GetArguments()
        {
            return Operators.SelectMany(x => x.GetArguments()).OrderBy(x => x).ToArray();
        }

        public override int GetOperatorNeededArguments()
        {
            return Operators.Count;
        }

        public override void Instantiate(IAsciiBasePropositionalOperator[] arguments)
        {
            
        }

        public override string ToLogicString()
        {
            if (Operators.Any())
            {
                return Name.ToString() + '(' + Operators.Aggregate(" ", (current, baseOperator) => current + baseOperator.ToLogicString() + " , ").TrimEnd(' ', ',') + " )";
            }
            return Name.ToString();;
        }

        public override IAsciiBasePropositionalOperator ToNandify()
        {
            var predicate = new PredicateOperator(_argumentManager) {Name = Name};
            foreach (var asciiBasePropositionalOperator in GetChilds())
            {
                predicate.AddChild(asciiBasePropositionalOperator.ToNandify());
            }
            return predicate;
        }

        public override IAsciiBasePropositionalOperator Negate()
        {
            var predicate = new PredicateOperator(_argumentManager) {Name = Name};
            foreach (var asciiBasePropositionalOperator in GetChilds())
            {
                predicate.AddChild(asciiBasePropositionalOperator.ToAndOrNot().Negate());
            }
            return predicate;
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            var predicate = new PredicateOperator(_argumentManager) {Name = Name};
            foreach (var asciiBasePropositionalOperator in GetChilds())
            {
                predicate.AddChild(asciiBasePropositionalOperator.ToAndOrNot());
            }
            return predicate;
        }

        public override string ToString()
        {
            if (Operators.Any())
            {
                return Name.ToString() + '(' + Operators.Aggregate(" ", (current, baseOperator) => current + baseOperator.ToString() + " , ").TrimEnd(' ', ',') + " )";
            }
            return Name.ToString();
        }
        
        public override IAsciiBasePropositionalOperator[] GetChilds()
        {
            return Operators.ToArray();
        }

        public char GetName()
        {
            return Name;
        }
        public override bool IsAdvanced()
        {
            return false;
        }
        
        public override string ToName()
        {
            return GetName()+"()";
        }
        public override bool Equals(object obj)
        {
            if (!(obj is PredicateOperator oper))
            {
                return false;
            }
            
            if (oper == this)
            {
                return true;
            }
            
            var childs = GetChilds();
            var objChilds = oper.GetChilds();
            
            if (childs.Length != objChilds.Length)
            {
                return false;
            }

            return !childs.Where((t, i) => !t.Equals(objChilds[i])).Any();
        }
        
        public override void UpdateChild(int index, IAsciiBasePropositionalOperator baseOperator)
        {
            if (index > Operators.Count)
            {
                throw new ArgumentException();
            }
            Operators[index] = baseOperator;
        }
    }
}