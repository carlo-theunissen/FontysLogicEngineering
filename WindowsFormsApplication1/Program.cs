using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using WindowsFormsApplication1.Decorators;

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


			//TESTING CODE
			StringParser parser = StringParser.Create(">(A, B)");



			TruthTableCreator table = new TruthTableCreator(parser);


			DisjunctiveNormalDecorator decorator = new DisjunctiveNormalDecorator(table);


			TruthTableCreator processed = new TruthTableCreator(decorator);

			
			foreach (byte[] row in table.GetTable())
			{
				foreach (byte colomn in row)
				{
					Debug.Write(colomn);
				}
				Debug.WriteLine("");
			}

			Debug.WriteLine("================");

			foreach (byte[] row in processed.GetTable())
			{
				foreach (byte colomn in row)
				{
					Debug.Write(colomn);
				}
				Debug.WriteLine("");
			}

			Debug.WriteLine(decorator.GetOperator().ToString());

	
		}
	}
}
