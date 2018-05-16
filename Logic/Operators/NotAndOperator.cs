﻿using Logic.Abstract;

namespace Logic.Operators
{
    internal class NotAndOperator : AbstractDubbleOperator
    {
        public NotAndOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override char GetAsciiSymbol()
        {
            return '%';
        }

        public override char GetLogicSymbol()
        {
            return '%';
        }

        public override bool Result()
        {
            return !(_A.Result() && _B.Result());
        }
    }
}