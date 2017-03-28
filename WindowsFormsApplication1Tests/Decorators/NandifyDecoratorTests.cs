using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApplication1.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Decorators.Tests
{
	[TestClass()]
	public class NandifyDecoratorTests
	{
		private void CheckFullTable(string parse)
		{
			StringParser parser = StringParser.Create(parse);
			SimplifiedTruthTableCreator table = new SimplifiedTruthTableCreator(parser);

			NandifyDecorator decorator = new NandifyDecorator(parser.GetOperator());
			SimplifiedTruthTableCreator processed = new SimplifiedTruthTableCreator(decorator);

			byte[][] orginal = table.GetTable();
			byte[][] calculated = processed.GetTable();

			for (int i = 0; i < orginal.Length; i++)
			{
				for (int j = 0; j < orginal[i].Length; j++)
				{
					if (orginal[i][j] != calculated[i][j])
					{
						Assert.Fail();
					}
				}
			}


		}
		[TestMethod()]
		public void NandifyDecoratorAdvancedTest()
		{
			CheckFullTable(">(A,&(>(B,=(A,E)),~(D)))");
		}
		[TestMethod()]
		public void NandifyDecoratorOrTest()
		{
			CheckFullTable("|(A,B)");
		}
		[TestMethod()]
		public void NandifyDecoratorAndTest()
		{
			CheckFullTable("&(A,B)");
		}
		[TestMethod()]
		public void NandifyDecoratorSameTest()
		{
			CheckFullTable("=(A,B)");
		}
		[TestMethod()]
		public void NandifyDecoratorNotTest()
		{
			CheckFullTable("~(A,B)");
		}
		[TestMethod()]
		public void NandifyDecoratorIfThenTest()
		{
			CheckFullTable(">(A,B)");
		}


	}
}