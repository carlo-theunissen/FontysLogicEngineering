using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.interfaces;
using System.Diagnostics;
using WindowsFormsApplication1.Utils;
using WindowsFormsApplication1.Abstract;

namespace WindowsFormsApplication1
{
	public class TruthTableCreator : AbstractTrueTable
	{

		public override byte[][] GetTable()
		{
			char[] names = _operator.GetArguments();

			List<byte[]> result = new List<byte[]>();
			foreach (bool[] data in GetAllOptions(names.Length))
			{
				byte[] array = new byte[data.Length + 1];
				for (int i = 0; i < data.Length; i++)
				{
					_manager.SetArgumentValue(names[i], data[i]);
					array[i] = (byte) (data[i] ? 1 : 0);
				}
				array[data.Length] = (byte)(_operator.Result() ? 1 : 0);
				result.Add(array);
			}
			return result.ToArray();
		}

		public override string ToHex()
		{
			throw new NotImplementedException();
		}
	}
}
