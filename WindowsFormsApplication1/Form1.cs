using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void kickoff_Click(object sender, EventArgs e)
		{
			StringParser parser = StringParser.Create(textBox1.Text);
			CreateAllTable(ref parser);
		}

		private void CreateAllTable(ref StringParser parser)
		{
			
		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
