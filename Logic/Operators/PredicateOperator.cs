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
            return this;
        }

        public override IAsciiBasePropositionalOperator ToDeMorgen()
        {
            return this;
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            return this;
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
            throw new System.NotImplementedException();
        }
        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
            var oper = obj as PredicateOperator;
            return 
                oper != null 
                && obj.GetType() == GetType()
                && (
                    (GetChilds()[0].Equals(oper.GetChilds()[0]) && GetChilds()[1].Equals(oper.GetChilds()[1]))
                    ||  (GetChilds()[1].Equals(oper.GetChilds()[0]) && GetChilds()[0].Equals(oper.GetChilds()[1])));
        }
    }
}