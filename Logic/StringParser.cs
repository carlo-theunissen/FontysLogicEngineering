using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Logic.Abstract;
using Logic.interfaces;

namespace Logic
{
    public class StringParser : AbstractParser
    {
        private readonly ArgumentsManager _argumentManager;
        private readonly string _data;

        private readonly int _startOffset;

        private IAsciiBaseOperator _operator;

        private int _ownOffset;
        private ParserState _state;

        private Dictionary<string, bool> _variableData;

        /**
         * Constructor of a string parser
         * @arg data The complete string that has to be parsed
         * @arg variable The offset of the provided string where the parser needs to start
         * @visibility private if you'd to intansiate a string parser, use  StringParser::create
         */
        private StringParser(string data, int offset, ArgumentsManager manager)
        {
            _argumentManager = manager;
            if (data.Length <= offset || offset < 0 || data[0] == '\0')
                throw new ArgumentOutOfRangeException();
            _data = data;
            _startOffset = offset;
            _state = ParserState.Unknown;

            StartParsing();
        }

        public override IArgumentController GetArgumentController()
        {
            return _argumentManager;
        }

        /**
         * Use this function to create an instance of StringParser.
         */
        public static StringParser Create(string data)
        {
            var manager = new ArgumentsManager();
            return new StringParser(data, 0, manager);
        }

        private char GetCurrentChar()
        {
            return _data.Substring(_startOffset + _ownOffset, 1).ToCharArray()[0];
        }

        private char GetNextChar()
        {
            _ownOffset++;
            return GetCurrentChar();
        }

        private void StartParsing()
        {
            do
            {
                switch (_state)
                {
                    //the first state, the parser doesn't know about the current value
                    case ParserState.Unknown:
                        HandleUnknownStage();

                        break;

                    //We are going to search for the first argument
                    case ParserState.ParseArguments:

                        ParseArguments();
                        _state = ParserState.Check;

                        break;

                    case ParserState.Check:
                        if (PostCheck())
                            return;
                        break;
                }
            } while (true);
        }

        private void ParseArguments()
        {
            var arguments = new IAsciiBaseOperator[_operator.GetOperatorNeededArguments()];
            for (var i = 0; i < _operator.GetOperatorNeededArguments(); i++)
            {
                while (!FindArgument())
                {
                }
                arguments[i] = GetArgument();
            }
            _operator.Instantiate(arguments);
        }

        private bool FindArgument()
        {
            if (GetCurrentChar() == '(' || GetCurrentChar() == ',')
            {
                GetNextChar();
                return true;
            }
            GetNextChar();
            return false;
        }

        private bool PostCheck()
        {
            if (_ownOffset + _startOffset + 1 >= _data.Length || GetCurrentChar() == ',' || GetCurrentChar() == ')')
                return true;
            GetNextChar();
            return false;
        }

        private bool HandleUnknownStage()
        {
            var use = GetCurrentChar();
            if (FindOperator(use))
            {
                _state = ParserState.ParseArguments;
                return true;
            }

            if (FindScalar(use))
            {
                _state = ParserState.Check;
                return true;
            }
            return false;
        }

        private bool FindOperator(char use)
        {
            _operator = _operatorFactory.GetOperator(use, _argumentManager);
            _ownOffset++;

            //The operatorfactory returns a operator if he finds it
            return _operator != null;
        }

        private bool FindScalar(char use)
        {
            var regex = new Regex(@"[a-zA-Z]");
            var match = regex.Match(use.ToString());

            if (match.Success)
            {
                _operator = _argumentManager.RequestOperator(use);

                return true;
            }
            return false;
        }

        private IAsciiBaseOperator GetArgument()
        {
            var searcher = new StringParser(_data, _startOffset + _ownOffset, _argumentManager);
            _ownOffset += searcher.GetOwnOffset();
            return searcher.GetOperator();
        }

        public override string ToString()
        {
            return _data.Substring(_startOffset);
            ;
        }

        public override IAsciiBaseOperator GetOperator()
        {
            return _operator;
        }

        public int GetOwnOffset()
        {
            return _ownOffset;
        }

        public void SetVariableData(Dictionary<string, bool> data)
        {
            _variableData = data;
        }


        private enum ParserState
        {
            Unknown,
            ParseArguments,
            Check
        }
    }
}