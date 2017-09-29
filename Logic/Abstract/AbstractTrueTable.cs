using System;
using System.Collections.Generic;
using Logic.interfaces;

namespace Logic.Abstract
{
    public abstract class AbstractTrueTable : ITruthTable
    {
        protected IArgumentController _manager;
        protected IAsciiBaseOperator _operator;
        protected IParser _parser;

        public AbstractTrueTable(IParser parser)
        {
            _manager = parser.GetArgumentController();
            _operator = parser.GetOperator();
            _parser = parser;
        }

        public abstract byte[][] GetTable();
        public abstract string ToHex();

        public IParser GetParser()
        {
            return _parser;
        }

        protected bool? GetResults(ref bool[] data)
        {
            var names = _operator.GetArguments();
            for (var i = 0; i < data.Length; i++)
                _manager.SetArgumentValue(names[i], data[i]);
            return _operator.Result();
        }

        protected bool[][] GetAllOptions(int length)
        {
            var result = new List<bool[]>();

            //calculate the amount of different options 
            var num = (int) Math.Pow(2, length) - 1;

            for (var i = 0; i <= num; i++)
            {
                var array = new bool[length];
                for (var pos = 0; pos < length; pos++)
                    array[pos] = (i & (1 << pos)) > 0;
                result.Add(array);
            }
            return result.ToArray();
        }
    }
}