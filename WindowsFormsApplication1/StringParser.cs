using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WindowsFormsApplication1.interfaces;
namespace WindowsFormsApplication1
{
	public class StringParser : IParser
	{

		private readonly int _startOffset = 0;
		private readonly String _data;
		private readonly ArgumentsManager _argumentManager;

		private int _ownOffset = 0;
		private ParserState _state;
		private Dictionary<String, bool> _variableData;
		
		private IAsciiBaseOperator _operator;
		public IArgumentController GetArgumentController()
		{
			return _argumentManager;
		}

		private readonly static OperatorFactory _operatorFactory;
		enum ParserState{
			Unknown, 
			ParseArguments,
			Check
		}
		/**
		 * Static constructor
		 */ 
		static StringParser()
		{
			_operatorFactory = new OperatorFactory();
		}


		/**
		 * Use this function to create an instance of StringParser.
		 */
		public static StringParser Create(String data)
		{
			ArgumentsManager manager = new ArgumentsManager();
			return new StringParser(data, 0, manager);
		}
		/**
		 * Constructor of a string parser
		 * @arg data The complete string that has to be parsed
		 * @arg variable The offset of the provided string where the parser needs to start
		 * @visibility private if you'd to intansiate a string parser, use  StringParser::create
		 */
		private StringParser(String data,  int offset, ArgumentsManager manager)
		{
			_argumentManager = manager;
			if (data.Length <= offset || offset < 0 || data[0] == '\0')
			{
				throw new ArgumentOutOfRangeException();
			}
			_data = data;
			_startOffset = offset;
			_state = ParserState.Unknown;

			StartParsing();
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
				
				switch (_state) {

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
						{
							return;
						}
						break; 
				}

				
			} while (true);
		}
		private void ParseArguments()
		{
			IAsciiBaseOperator[] arguments = new IAsciiBaseOperator[_operator.GetOperatorNeededArguments()];
			for (int i = 0; i < _operator.GetOperatorNeededArguments(); i++)
			{
				while(!FindArgument()){}
				arguments[i] =  GetArgument();
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
			
			if ( _ownOffset + _startOffset + 1 >= _data.Length || GetCurrentChar() == ',' || GetCurrentChar() == ')')
			{
				return true;
			}
			GetNextChar();
			return false;


		}

		private bool HandleUnknownStage()
		{
			char use = GetCurrentChar();
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
			return (_operator != null);
		}

		private bool FindScalar(char use)
		{
			Regex regex = new Regex(@"[a-zA-Z]");
			Match match = regex.Match(use.ToString());
			
			if (match.Success)
			{
				_operator = _argumentManager.RequestOperator(use);

				return true;
			}
			return false;
		}

		private IAsciiBaseOperator GetArgument()
		{

			StringParser searcher = new StringParser(_data, _startOffset + _ownOffset, _argumentManager);
			_ownOffset += searcher.GetOwnOffset();
			return searcher.GetOperator();
		}

		public override string ToString()
		{
			return _data.Substring(_startOffset); ;
		}
		public IAsciiBaseOperator GetOperator()
		{
			return _operator;
		}
		public int GetOwnOffset()
		{
			return _ownOffset;
		}
		public void SetVariableData(Dictionary<String, bool> data)
		{
			_variableData = data;
		}


	}
}
