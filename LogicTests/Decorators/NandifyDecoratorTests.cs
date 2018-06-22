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
            var table = new SimplifiedTruthTableCreator(parser.GetOperator());
            var processed = new SimplifiedTruthTableCreator(parser.GetOperator().ToNandify());

            var orginal = table.GetTable();
            var calculated = processed.GetTable();

            for (var i = 0; i < orginal.Length; i++)
            for (var j = 0; j < orginal[i].Length; j++)
                Assert.True(orginal[i][j] == calculated[i][j]);
        }

        [Fact]
        public void NandifyDecoratorAndTest()
        {
            CheckFullTable("&(a,b)");
        }

        [Fact]
        public void NandifyDecoratorIfThenTest()
        {
            CheckFullTable(">(a,b)");
        }

        [Fact]
        public void NandifyDecoratorNotTest()
        {
            CheckFullTable("~(a,b)");
        }

        [Fact]
        public void NandifyDecoratorOrTest()
        {
            CheckFullTable("|(a,b)");
        }
    }
}