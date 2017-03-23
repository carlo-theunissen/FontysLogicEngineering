using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Exception;
using WindowsFormsApplication1.Operators;

namespace WindowsFormsApplication1.Tests
{
	[TestClass()]
	public class ScalarOperatorTests
	{
		[TestMethod()]
		public void ScalarOperatorTest()
		{
			ArgumentsManager manager = new ArgumentsManager();
			ScalarOperator oper = new ScalarOperator('t', manager);
		}

		[TestMethod()]
		public void GetChildsTest()
		{
			ArgumentsManager manager = new ArgumentsManager();
			ScalarOperator oper = new ScalarOperator('t', manager);
			Assert.IsNull(oper.GetChilds());
		}

		[TestMethod()]
		public void ResultTrueTest()
		{
			ArgumentsManager manager = new ArgumentsManager();
			ScalarOperator oper = new ScalarOperator('t', manager);
			oper.SetValue(true);
			Assert.IsTrue(oper.Result());
		}

		[TestMethod()]
		[ExpectedException(typeof(ScalarInvalidValue))]
		public void ResultNullTest()
		{
			ArgumentsManager manager = new ArgumentsManager();
			ScalarOperator oper = new ScalarOperator('t', manager);
			oper.Result();
		}
	}
}