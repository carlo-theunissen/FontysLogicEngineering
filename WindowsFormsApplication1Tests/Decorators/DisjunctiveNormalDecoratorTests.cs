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
	public class DisjunctiveNormalDecoratorTests
	{

		[TestMethod()]
		public void CompleteDisjunctveDecoratorTest()
		{
			StringParser parser = StringParser.Create("|(|(A,B), C)");



			TruthTableCreator table = new TruthTableCreator(parser);


			DisjunctiveNormalDecorator decorator = new DisjunctiveNormalDecorator(table);


			TruthTableCreator processed = new TruthTableCreator(decorator);

			byte[][] dataOriginal = table.GetTable();
			byte[][] dataProcessed = processed.GetTable();
			for (int i = 0; i < dataOriginal.Length; i++)
			{
				for (int j = 0; j < dataOriginal[i].Length; j++)
				{
					if (dataOriginal[i][j] != dataProcessed[i][j])
					{
						Assert.Fail();
					}
				}
			}

		}
	}
}