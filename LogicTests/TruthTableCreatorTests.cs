using Logic;
using Xunit;

namespace LogicTests
{
    public class TruthTableCreatorTests
    {
        private void CheckFullTable(string parse)
        {
            var parser = StringParser.Create(parse);
            var table = new TruthTableCreator(parser.GetOperator());
            var manager = parser.GetArgumentController();

            foreach (var data in table.GetTable())
            {
                var names = parser.GetOperator().GetArguments();
                for (var i = 0; i < names.Length; i++)
                    manager.SetArgumentValue(names[i], data[i] == 1);
                Assert.False(parser.GetOperator().Result() != (data[data.Length - 1] == 1));
            }
        }

        [Fact]
        public void GetFullTableTest()
        {
            CheckFullTable("|(|(a,b), ~(c))");
        }

        [Fact]
        public void GetFullTableTest2()
        {
            CheckFullTable("|(|(a,b), c)");
        }
    }
}