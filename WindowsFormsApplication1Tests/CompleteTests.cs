using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApplication1;
namespace WindowsFormsApplication1Tests
{
	[TestClass]
	public class CompleteTests
	{
		[TestMethod()]
		public void SimpleBiggerTestSuccess()
		{
			StringParser parser = StringParser.Create(">( A, B)");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', false);
			Assert.IsTrue(parser.ToOperator().Result());

		}

		[TestMethod()]
		public void SimpleBiggerTestFail()
		{
			StringParser parser = StringParser.Create(">( A, B)");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', false);
			manager.SetArgumentValue('B', true);
			Assert.IsFalse(parser.ToOperator().Result());

		}
		[TestMethod()]
		public void SimpleLessTestSuccess()
		{
			StringParser parser = StringParser.Create("<( A, B)");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', false);
			manager.SetArgumentValue('B', true);
			Assert.IsTrue(parser.ToOperator().Result());

		}

		[TestMethod()]
		public void SimpleLessTestFail()
		{
			StringParser parser = StringParser.Create("<( A, B)");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', false);
			Assert.IsFalse(parser.ToOperator().Result());

		}
		[TestMethod()]
		public void SimpleSameTestSuccess()
		{
			StringParser parser = StringParser.Create("=( A, B)");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', true);
			Assert.IsTrue(parser.ToOperator().Result());

		}
		[TestMethod()]
		public void SimpleSameTestFail()
		{
			StringParser parser = StringParser.Create("=( A, B)");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', false);
			Assert.IsFalse(parser.ToOperator().Result());

		}
		[TestMethod()]
		public void SimpleOrTestSuccess()
		{
			StringParser parser = StringParser.Create("|( A, B)");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', false);
			Assert.IsTrue(parser.ToOperator().Result());

		}
		[TestMethod()]
		public void SimpleOrTestFail()
		{
			StringParser parser = StringParser.Create("|( A, B)");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', false);
			manager.SetArgumentValue('B', false);
			Assert.IsFalse(parser.ToOperator().Result());
		}

		[TestMethod()]
		public void SimpleAndTestSuccess()
		{
			StringParser parser = StringParser.Create("&( A, B)");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', true);
			Assert.IsTrue(parser.ToOperator().Result());

		}
		[TestMethod()]
		public void SimpleNotTestSuccess()
		{
			StringParser parser = StringParser.Create("~( A )");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', false);
			Assert.IsTrue(parser.ToOperator().Result());

		}

		[TestMethod()]
		public void SimpleAndTestFail()
		{
			StringParser parser = StringParser.Create("&( A, B)");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', false);
			manager.SetArgumentValue('B', false);
			Assert.IsFalse(parser.ToOperator().Result());
		}
		[TestMethod()]
		public void NestedAndOr()
		{
			StringParser parser = StringParser.Create("&( |( A, B), A)");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', false);
			Assert.IsTrue(parser.ToOperator().Result());
		}
		[TestMethod()]
		public void NestedLarge()
		{
			StringParser parser = StringParser.Create("=( >(A,B), |(A ,B) ))");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', false);
			Assert.IsTrue(parser.ToOperator().Result());
		}
		[TestMethod()]
		public void NestedLarge2()
		{
			StringParser parser = StringParser.Create("=( >(A,B), |( ~(A) ,B) )");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', false);
			Assert.IsFalse(parser.ToOperator().Result());
		}



	}
}
