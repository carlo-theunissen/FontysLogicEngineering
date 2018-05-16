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

        private IAsciiBaseOperator _operator;


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

        public override IAsciiBaseOperator GetOperator()
        {
            return _operator;
        }

        private void StartCalculating()
        {
            var table = _data.GetTable();
            var outcome = new List<IAsciiBaseOperator>();

            foreach (var row in table)
                if (row[row.Length - 1] == 1)
                {
                    var list = new List<IAsciiBaseOperator>();
                    for (var x = 0; x < _names.Length; x++)
                    {
                        if (row[x] < 2)
                        {
                            list.Add(GetOperator(row[x], _names[x]));
                        }
                    }
                    outcome.Add(list.Any()
                        ? CombineOperators(list.ToArray(), '&')
                        : new TrueOperator(_argumentManager));
                }

            _operator = outcome.Any() ? CombineOperators(outcome.ToArray(), '|') : new FalseOperator(_argumentManager);
        }

        private IAsciiBaseOperator CombineOperators(IAsciiBaseOperator[] opers, char glueName)
        {
            return CalculateCombine(opers, 0, glueName);
        }

        private IAsciiBaseOperator CalculateCombine(IAsciiBaseOperator[] row, int index, char glueName)
        {
            //we are the last
            if (index + 1 == row.Length)
                return row[index];


            var glue = _operatorFactory.GetOperator(glueName, _argumentManager);
            glue.Instantiate(new[] {row[index], CalculateCombine(row, index + 1, glueName)});

            return glue;
        }

        private IAsciiBaseOperator GetOperator(byte value, char name)
        {
            switch (value)
            {
                case 0:
                    IAsciiBaseOperator oper = new NotOperator(_argumentManager);
                    oper.Instantiate(new IAsciiBaseOperator[] {_argumentManager.RequestOperator(name)});
                    return oper;

                case 1:
                    return _argumentManager.RequestOperator(name);
            }
            return null;
        }
    }
}