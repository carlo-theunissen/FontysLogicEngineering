using Logic;
using Logic.Exception;
using Logic.Operators;
using Xunit;

namespace LogicTests
{
    public class ScalarOperatorTests
    {
        [Fact]
        public void GetChildsTest()
        {
            var manager = new ArgumentsManager();
            var oper = new ScalarOperator('t', manager);
            Assert.Null(oper.GetChilds());
        }

        [Fact]
        public void ResultNullTest()
        {
            var manager = new ArgumentsManager();
            var oper = new ScalarOperator('t', manager);
            Assert.Throws<ScalarInvalidValue>(() => oper.Result());
        }

        [Fact]
        public void ResultTrueTest()
        {
            var manager = new ArgumentsManager();
            var oper = new ScalarOperator('t', manager);
            oper.SetValue(true);
            Assert.True(oper.Result());
        }

        [Fact]
        public void ScalarOperatorTest()
        {
            var manager = new ArgumentsManager();
            var oper = new ScalarOperator('t', manager);
        }
    }
}