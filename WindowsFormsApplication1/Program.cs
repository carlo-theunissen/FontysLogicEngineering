using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			StringParser parser = StringParser.Create("&( A, B)");
			ArgumentsManager manager = parser.ToOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', true);
			bool result = (parser.ToOperator().Result());
		}
	}
}
