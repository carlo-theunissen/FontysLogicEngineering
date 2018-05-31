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

        public override string ToString()
        {
            if (Operators.Any())
            {
                return Name.ToString() + '(' + Operators.Aggregate(" ", (current, baseOperator) => current + baseOperator.ToString() + " , ").TrimEnd(' ', ',') + " )";
            }
            return Name.ToString();;
        }
        
        public override IAsciiBasePropositionalOperator[] GetChilds()
        {
            return Operators.ToArray();
        }

        public char GetName()
        {
            return Name;
        }
    }
}