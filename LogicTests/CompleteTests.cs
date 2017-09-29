using Logic;
using Xunit;

namespace LogicTests
{
    public class CompleteTests
    {
        [Fact]
        public void NestedAndOr()
        {
            var parser = StringParser.Create("&( |( A, B), A)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', true);
            manager.SetArgumentValue('B', false);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void NestedLarge()
        {
            var parser = StringParser.Create("=( >(A,B), |(A ,B) ))");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', true);
            manager.SetArgumentValue('B', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void NestedLarge2()
        {
            var parser = StringParser.Create("=( >(A,B), |( ~(A) ,B) )");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', true);
            manager.SetArgumentValue('B', false);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleAndTestFail()
        {
            var parser = StringParser.Create("&( A, B)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', false);
            manager.SetArgumentValue('B', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleAndTestSuccess()
        {
            var parser = StringParser.Create("&( A, B)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', true);
            manager.SetArgumentValue('B', true);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleBiggerTestFail()
        {
            var parser = StringParser.Create(">( A, B)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', true);
            manager.SetArgumentValue('B', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleBiggerTestSuccess()
        {
            var parser = StringParser.Create(">( A, B)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', false);
            manager.SetArgumentValue('B', true);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleNotTestSuccess()
        {
            var parser = StringParser.Create("~( A )");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', false);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleOrTestFail()
        {
            var parser = StringParser.Create("|( A, B)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', false);
            manager.SetArgumentValue('B', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleOrTestSuccess()
        {
            var parser = StringParser.Create("|( A, B)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', true);
            manager.SetArgumentValue('B', false);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleSameTestFail()
        {
            var parser = StringParser.Create("=( A, B)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', true);
            manager.SetArgumentValue('B', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleSameTestSuccess()
        {
            var parser = StringParser.Create("=( A, B)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('A', true);
            manager.SetArgumentValue('B', true);
            Assert.True(parser.GetOperator().Result());
        }
    }
}