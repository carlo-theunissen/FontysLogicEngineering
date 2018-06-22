using System.Collections.Generic;
using Logic;
using Logic.Exception;
using Logic.Operators;
using Xunit;

namespace LogicTests
{
    public class StringParserTests
    {
        [Fact]
        public void ArgumentManagerChange()
        {
            var parser = StringParser.Create(">(a,b)");
            var manager = parser.GetOperator().GetArgumentsManager();

            manager.SetArgumentValue('a', false);
            manager.SetArgumentValue('b', true);

            var result = parser.GetOperator().Result();

            manager.SetArgumentValue('a', true);
            manager.SetArgumentValue('b', false);

            Assert.True(result != parser.GetOperator().Result());
        }

        [Fact]
        public void GetArgumentsFromOperator()
        {
            var parser = StringParser.Create(">(a ,b");
            var ope = parser.GetOperator();

            var check = new List<char>(ope.GetArguments());
            Assert.True(check.Contains('a') && check.Contains('b'));
        }

        [Fact]
        public void GreaterThenOperatorFailSame()
        {
            var parser = StringParser.Create(">(a,b)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', true);
            manager.SetArgumentValue('b', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void GreaterThenOperatorNestedFail()
        {
            var parser = StringParser.Create(">(a,>(a,b))");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', true);
            manager.SetArgumentValue('b', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void GreaterThenOperatorSuccess()
        {
            var parser = StringParser.Create(">(a,b)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', true);
            manager.SetArgumentValue('b', true);
            Assert.True(parser.GetOperator().Result());
        }


        [Fact]
        public void SingleScalerReceive()
        {
            var parser = StringParser.Create("a");
            Assert.True(parser.GetOperator() is ScalarPropositionalOperator);
        }

        [Fact]
        public void SingleScalerReceiveValueChangeFalse()
        {
            var parser = StringParser.Create("a");
            parser.GetOperator().GetArgumentsManager().SetArgumentValue('a', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void SingleScalerReceiveValueChangeTrue()
        {
            var parser = StringParser.Create("a");
            parser.GetOperator().GetArgumentsManager().SetArgumentValue('a', true);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void SingleScalerReceiveValueNonChange()
        {
            var parser = StringParser.Create("a");
            Assert.Throws<ScalarInvalidValue>(() => parser.GetOperator().Result());
        }
    }
}