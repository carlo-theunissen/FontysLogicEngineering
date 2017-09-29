using Logic;
using Logic.interfaces;
using Logic.Operators;
using Xunit;

namespace LogicTests.Operators
{
    public class IfThenOperatorTest
    {
        [Fact]
        public void ResultInvalidLowerTest()
        {
            var manager = new ArgumentsManager();
            var one = new ScalarOperator('o', manager);
            one.SetValue(true);


            var zero = new ScalarOperator('z', manager);
            zero.SetValue(false);

            var opr = new IfThenOperator(manager);
            IAsciiBaseOperator[] arguments = {one, zero};
            opr.Instantiate(arguments);
            Assert.False(opr.Result());
        }

        [Fact]
        public void ResultInvalidSameTest()
        {
            var manager = new ArgumentsManager();
            var one = new ScalarOperator('o', manager);
            one.SetValue(true);

            var zero = new ScalarOperator('z', manager);
            zero.SetValue(false);

            var opr = new IfThenOperator(manager);
            IAsciiBaseOperator[] arguments = {one, zero};
            opr.Instantiate(arguments);
            Assert.False(opr.Result());
        }

        [Fact]
        public void ResultValidTest()
        {
            var manager = new ArgumentsManager();
            var one = new ScalarOperator('o', manager);
            one.SetValue(false);

            var zero = new ScalarOperator('z', manager);
            zero.SetValue(true);


            var opr = new IfThenOperator(manager);

            IAsciiBaseOperator[] arguments = {one, zero};
            opr.Instantiate(arguments);

            Assert.True(opr.Result());
        }
    }
}