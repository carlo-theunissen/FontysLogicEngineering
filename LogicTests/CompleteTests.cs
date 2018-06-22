using Logic;
using Xunit;

namespace LogicTests
{
    public class CompleteTests
    {
        [Fact]
        public void NestedAndOr()
        {
            var parser = StringParser.Create("&( |( a, b), a)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', true);
            manager.SetArgumentValue('b', false);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void NestedLarge()
        {
            var parser = StringParser.Create("=( >(a,b), |(a ,b) ))");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', true);
            manager.SetArgumentValue('b', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void NestedLarge2()
        {
            var parser = StringParser.Create("=( >(a,b), |( ~(a) ,b) )");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', true);
            manager.SetArgumentValue('b', false);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleAndTestFail()
        {
            var parser = StringParser.Create("&( a, b)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', false);
            manager.SetArgumentValue('b', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleAndTestSuccess()
        {
            var parser = StringParser.Create("&( a, b)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', true);
            manager.SetArgumentValue('b', true);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleBiggerTestFail()
        {
            var parser = StringParser.Create(">( a, b)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', true);
            manager.SetArgumentValue('b', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleBiggerTestSuccess()
        {
            var parser = StringParser.Create(">( a, b)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', false);
            manager.SetArgumentValue('b', true);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleNotTestSuccess()
        {
            var parser = StringParser.Create("~( a )");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', false);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleOrTestFail()
        {
            var parser = StringParser.Create("|( a, b)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', false);
            manager.SetArgumentValue('b', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleOrTestSuccess()
        {
            var parser = StringParser.Create("|( a, b)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', true);
            manager.SetArgumentValue('b', false);
            Assert.True(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleSameTestFail()
        {
            var parser = StringParser.Create("=( a, b)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', true);
            manager.SetArgumentValue('b', false);
            Assert.False(parser.GetOperator().Result());
        }

        [Fact]
        public void SimpleSameTestSuccess()
        {
            var parser = StringParser.Create("=( a, b)");
            var manager = parser.GetOperator().GetArgumentsManager();
            manager.SetArgumentValue('a', true);
            manager.SetArgumentValue('b', true);
            Assert.True(parser.GetOperator().Result());
        }
    }
}