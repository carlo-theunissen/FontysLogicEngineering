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
            var parser = StringParser.Create(">(A,B)");
            var manager = parser.GetOperator().GetArgumentsManager();

            manager.SetArgumentValue('A', false);
            manager.SetArgumentValue('B', true);

            var result = parser.GetOperator().Result();

            manager.SetArgumentValue('A', true);
            manager.SetArgumentValue('B', false);

            Assert.True(result != parser.GetOperator().Result());
        }

        [Fact]
        public void GetArgumentsFromOperator()
        {
            var parser = StringParser.Create(">(A ,B");
            var ope = parser.GetOperator();

            var check = new List<char>(ope.GetArguments());
            Assert.True(check.Contains('A') && check.Contains('B'));
        }

        [Fact]
        public void GreaterThenOperatorFailSame()
        {
            var parser = StringParser.Create(">(A,B)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', true);
            manager.SetArgumentValue('B', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void GreaterThenOperatorNestedFail()
        {
            var parser = StringParser.Create(">(A,>(A,B))");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', true);
            manager.SetArgumentValue('B', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void GreaterThenOperatorSuccess()
        {
            var parser = StringParser.Create(">(A,B)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', true);
            manager.SetArgumentValue('B', true);
            Assert.True(parser.GetOperator().Result());
        }


        [Fact]
        public void SingleScalerReceive()
        {
            var parser = StringParser.Create("A");
            Assert.True(parser.GetOperator() is ScalarPropositionalOperator);
        }

        [Fact]
        public void SingleScalerReceiveValueChangeFalse()
        {
            var parser = StringParser.Create("A");
            parser.GetOperator().GetArgumentsManager().SetArgumentValue('A', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void SingleScalerReceiveValueChangeTrue()
        {
            var parser = StringParser.Create("A");
            parser.GetOperator().GetArgumentsManager().SetArgumentValue('A', true);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void SingleScalerReceiveValueNonChange()
        {
            var parser = StringParser.Create("A");
            Assert.Throws<ScalarInvalidValue>(() => parser.GetOperator().Result());
        }
    }
}