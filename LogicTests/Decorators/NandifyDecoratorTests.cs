using Logic;
using Logic.Decorators;
using Xunit;

namespace LogicTests.Decorators
{
    public class NandifyDecoratorTests
    {
        private void CheckFullTable(string parse)
        {
            var parser = StringParser.Create(parse);
            var table = new SimplifiedTruthTableCreator(parser);

            var decorator = new NandifyDecorator(parser.GetOperator());
            var processed = new SimplifiedTruthTableCreator(decorator);

            var orginal = table.GetTable();
            var calculated = processed.GetTable();

            for (var i = 0; i < orginal.Length; i++)
            for (var j = 0; j < orginal[i].Length; j++)
                Assert.True(orginal[i][j] == calculated[i][j]);
        }

        [Fact]
        public void NandifyDecoratorAdvancedTest()
        {
            CheckFullTable(">(A,&(>(B,=(A,E)),~(D)))");
        }

        [Fact]
        public void NandifyDecoratorAndTest()
        {
            CheckFullTable("&(A,B)");
        }

        [Fact]
        public void NandifyDecoratorIfThenTest()
        {
            CheckFullTable(">(A,B)");
        }

        [Fact]
        public void NandifyDecoratorNotTest()
        {
            CheckFullTable("~(A,B)");
        }

        [Fact]
        public void NandifyDecoratorOrTest()
        {
            CheckFullTable("|(A,B)");
        }

        [Fact]
        public void NandifyDecoratorSameTest()
        {
            CheckFullTable("=(A,B)");
        }
    }
}