using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
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
			
			/*
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
			*/

			StringParser parser = StringParser.Create("|(|(A,B), C)");


			TruthTableCreator table = new TruthTableCreator(parser);
			foreach (byte[] result in table.GetSimpleTable())
			{
				foreach (byte single in result)
				{
					Debug.Write(single);
				}
				Debug.WriteLine("");
			}
		}
	}
}
