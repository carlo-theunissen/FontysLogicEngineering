using System.Linq;
using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class NotPropositionalOperator : AbstractSinglePropositionalOperator
    {
        private bool isAdvance;
        public NotPropositionalOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override bool Result()
        {
            return !_A.Result();
        }

        public override IAsciiBasePropositionalOperator ToNandify()
        {
            var nand = new NotAndPropositionalOperator(_argumentManager);
            var childNand = GetChilds()[0].ToNandify();
            nand.Instantiate(new [] {childNand, childNand});
            return nand;
        }

        public override IAsciiBasePropositionalOperator Negate()
        {
            return GetChilds()[0];
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            var not = new NotPropositionalOperator(_argumentManager);
            not.Instantiate(GetChilds().Select( x => x.ToAndOrNot()).ToArray());
            return not;
        }

        public override char GetAsciiSymbol()
        {
            return '~';
        }

        public override char GetLogicSymbol()
        {
            return '¬';
        }

        public override void Instantiate(IAsciiBasePropositionalOperator[] arg)
        {
            base.Instantiate(arg);
            isAdvance = _A.IsAdvanced() || (_A is FalsePropositionalOperator || _A is TruePropositionalOperator);
        }

        public override bool IsAdvanced()
        {
            return isAdvance;
        }
    }
}