using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Logic.Abstract;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Decorators
{
    public class DisjunctiveNormalDecorator : AbstractParser
    {
        private readonly ArgumentsManager _argumentManager;
        private readonly ITruthTable _data;
        private readonly char[] _names;

        private IAsciiBasePropositionalOperator _propositionalOperator;


        public DisjunctiveNormalDecorator(ITruthTable table)
        {
            _data = table;
            _argumentManager = new ArgumentsManager();
            _names = table.GetParser().GetOperator().GetArguments();
            StartCalculating();
        }

        public override IArgumentController GetArgumentController()
        {
            return _argumentManager;
        }

        public override IAsciiBasePropositionalOperator GetOperator()
        {
            return _propositionalOperator;
        }

        private void StartCalculating()
        {
            var table = _data.GetTable();
            var outcome = new List<IAsciiBasePropositionalOperator>();

            foreach (var row in table)
                if (row[row.Length - 1] == 1)
                {
                    var list = new List<IAsciiBasePropositionalOperator>();
                    for (var x = 0; x < _names.Length; x++)
                    {
                        if (row[x] < 2)
                        {
                            list.Add(GetOperator(row[x], _names[x]));
                        }
                    }
                    outcome.Add(list.Any()
                        ? CombineOperators(list.ToArray(), '&')
                        : new TruePropositionalOperator(_argumentManager));
                }

            _propositionalOperator = outcome.Any() ? CombineOperators(outcome.ToArray(), '|') : new FalsePropositionalOperator(_argumentManager);
        }

        private IAsciiBasePropositionalOperator CombineOperators(IAsciiBasePropositionalOperator[] opers, char glueName)
        {
            return CalculateCombine(opers, 0, glueName);
        }

        private IAsciiBasePropositionalOperator CalculateCombine(IAsciiBasePropositionalOperator[] row, int index, char glueName)
        {
            //we are the last
            if (index + 1 == row.Length)
                return row[index];


            var glue = _operatorFactory.GetOperator(glueName, _argumentManager);
            glue.Instantiate(new[] {row[index], CalculateCombine(row, index + 1, glueName)});

            return glue;
        }

        private IAsciiBasePropositionalOperator GetOperator(byte value, char name)
        {
            switch (value)
            {
                case 0:
                    IAsciiBasePropositionalOperator oper = new NotPropositionalOperator(_argumentManager);
                    oper.Instantiate(new IAsciiBasePropositionalOperator[] {_argumentManager.RequestOperator(name)});
                    return oper;

                case 1:
                    return _argumentManager.RequestOperator(name);
            }
            return null;
        }
    }
}