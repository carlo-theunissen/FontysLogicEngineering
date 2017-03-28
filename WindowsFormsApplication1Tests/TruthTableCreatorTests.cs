using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.interfaces;

namespace WindowsFormsApplication1.Tests
{
	[TestClass()]
	public class TruthTableCreatorTests
	{

		private void CheckFullTable(string parse)
		{
			StringParser parser = StringParser.Create(parse);
			TruthTableCreator table = new TruthTableCreator(parser);
			IArgumentController manager = parser.GetArgumentController();

			foreach (byte[] data in table.GetTable())
			{
				char[] names = parser.GetOperator().GetArguments();
				for (int i = 0; i < names.Length; i++)
				{
					manager.SetArgumentValue(names[i], data[i] == 1 );
				}
				if (parser.GetOperator().Result() != (data[data.Length - 1] == 1 ))
				{
					Assert.Fail();
				}
			}

		}
		[TestMethod()]
		public void GetFullTableTest()
		{
			CheckFullTable("|(|(A,B), ~(C))");
		}
		[TestMethod()]
		public void GetFullTableTest2()
		{
			CheckFullTable("|(|(A,B), C)");

		}

	}
}