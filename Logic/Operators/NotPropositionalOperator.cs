using Logic.Abstract;
using Logic.interfaces;

namespace Logic.Operators
{
    public class NotPropositionalOperator : AbstractSinglePropositionalOperator
    {
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

        public override IAsciiBasePropositionalOperator ToDeMorgen()
        {
            return GetChilds()[0];
        }

        public override IAsciiBasePropositionalOperator ToAndOrNot()
        {
            return this;
        }

        public override char GetAsciiSymbol()
        {
            return '~';
        }

        public override char GetLogicSymbol()
        {
            return '¬';
        }
        public override bool IsAdvanced()
        {
            return GetChilds()[0].IsAdvanced();
        }
    }
}