using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Abstract;
using Logic.interfaces;

namespace Logic
{
    public class TruthTableCreator : AbstractTrueTable
    {
        public TruthTableCreator(IAsciiBasePropositionalOperator oper) : base(oper)
        {
        }

        public override byte[][] GetTable()
        {
            var names = _operator.GetArguments();

            var result = new List<byte[]>();
            foreach (var data in GetAllOptions(names.Length))
            {
                var array = new byte[data.Length + 1];
                for (var i = 0; i < data.Length; i++)
                {
                    _manager.SetArgumentValue(names[i], data[i]);
                    array[i] = (byte) (data[i] ? 1 : 0);
                }
                array[data.Length] = (byte) (_operator.Result() ? 1 : 0);
                result.Add(array);
            }
            return result.ToArray();
        }

    }
}