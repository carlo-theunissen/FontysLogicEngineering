using System.Collections.Generic;
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
                    var opers = new IAsciiBaseOperator[_names.Length];
                    for (var x = 0; x < _names.Length; x++)
                        opers[x] = GetOperator(row[x], _names[x]);
                    outcome.Add(CombineOperators(opers, '&'));
                }

            _operator = CombineOperators(outcome.ToArray(), '|');
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

                case 2:
                    IAsciiBaseOperator not = new NotOperator(_argumentManager);
                    not.Instantiate(new IAsciiBaseOperator[] {_argumentManager.RequestOperator(name)});

                    IAsciiBaseOperator or = new OrOperator(_argumentManager);
                    or.Instantiate(new[] {_argumentManager.RequestOperator(name), not});

                    return or;
            }
            return null;
        }
    }
}