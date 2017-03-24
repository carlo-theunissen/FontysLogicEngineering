using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.interfaces;

namespace WindowsFormsApplication1.Operators.Tests
{
	[TestClass()]
	public class GreaterThenOperatorTests
	{
		[TestMethod()]
		public void ResultValidTest()
		{
			ArgumentsManager manager = new ArgumentsManager();
			ScalarOperator one = new ScalarOperator('o', manager);
			one.SetValue(true);

			ScalarOperator zero = new ScalarOperator('z', manager);
			zero.SetValue(false);



			GreaterThenOperator opr = new GreaterThenOperator(manager);

			IAsciiBaseOperator[] arguments = { one, zero };
			opr.Instantiate(arguments);

			Assert.IsTrue(opr.Result());
		}
		public void ResultInvalidSameTest()
		{
			ArgumentsManager manager = new ArgumentsManager();
			ScalarOperator one = new ScalarOperator('o', manager);
			one.SetValue(true);

			ScalarOperator zero = new ScalarOperator('z', manager);
			zero.SetValue(false);

			GreaterThenOperator opr = new GreaterThenOperator(manager);
			IAsciiBaseOperator[] arguments = { one, zero };
			opr.Instantiate(arguments);
			Assert.IsTrue(opr.Result());
		}
		public void ResultInvalidLowerTest()
		{
			ArgumentsManager manager = new ArgumentsManager();
			ScalarOperator one = new ScalarOperator('o', manager);
			one.SetValue(true);
			

			ScalarOperator zero = new ScalarOperator('z', manager);
			zero.SetValue(false);

			GreaterThenOperator opr = new GreaterThenOperator(manager);
			IAsciiBaseOperator[] arguments = { one, zero };
			opr.Instantiate(arguments);
			Assert.IsTrue(opr.Result());
		}
	}
}