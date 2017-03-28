using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Abstract;
using WindowsFormsApplication1.interfaces;
using WindowsFormsApplication1.Utils;

namespace WindowsFormsApplication1
{
	public class SimplifiedTruthTableCreator : AbstractTrueTable
	{
		public SimplifiedTruthTableCreator(IParser parser) : base(parser)
		{
		}

		private void GetSuccessFailList(out ICollection<bool[]> success, out ICollection<bool[]> fail, ref bool[][] data)
		{
			success = new List<bool[]>();
			fail = new List<bool[]>();
			foreach (bool[] items in data)
			{
				bool[] clone = (bool[])items.Clone();
				bool? outcome = GetResults(ref clone);
				if (outcome != null)
				{
					if (outcome.Value)
					{
						success.Add(items);
					}
					else
					{
						fail.Add(items);
					}
				}
			}
		}

		private byte[][] TransformArray(bool[][] data)
		{
			byte[][] result = new byte[data.Length][];
			for (int i = 0; i < data.Length; i++)
			{
				result[i] = new byte[data[i].Length];
				for (int j = 0; j < data[i].Length; j++)
				{

					result[i][j] = (byte)(data[i][j] ? 1 : 0);
				}
			}
			return result;
		}

		private byte[][] SimplfyList(byte[][] data)
		{
			ICollection<byte[]> newData = new List<byte[]>();
			byte[][] clonedData = (byte[][])data.Clone();

			foreach (byte[] current in data)
			{
				foreach (byte[] check in data)
				{
					if (check.Equals(current)) { continue; }

					int[] differ = ArrayUtils.GetDifferIndexes(current, check);

					if (differ.Length == 1)
					{
						byte[] temp = (byte[])check.Clone();
						temp[differ[0]] = 2;
						if (!ArrayUtils.ContainsArrayInList(temp, ref newData))
						{
							newData.Add(temp);
						}
					}
				}
			}

			if (newData.Count == 0)
			{
				return data;
			}
			return SimplfyList(newData.ToArray());
		}


		public override byte[][] GetTable()
		{
			ICollection<bool[]> success;
			ICollection<bool[]> fail;

			bool[][] data = GetAllOptions(_operator.GetArguments().Length);

			GetSuccessFailList(out success, out fail, ref data);

			List<byte[]> result = new List<byte[]>();
			for (int i = 0; i < 2; i++)
			{
				bool[][] collection = i == 0 ? fail.ToArray() : success.ToArray();

				foreach (byte[] items in SimplfyList(TransformArray(collection)))
				{
					byte[] array = new byte[items.Length + 1];
					Array.Copy(items, array, items.Length);
					array[items.Length] = (byte)i;
					result.Add(array);
				}
			}

			return result.ToArray();
		}

		public override string ToHex()
		{
			throw new NotImplementedException();
		}
	}
}
