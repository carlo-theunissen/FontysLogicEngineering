using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Logic.Abstract;
using Logic.interfaces;
using Logic.Utils;

namespace Logic
{
    public class SimplifiedTruthTableCreator : AbstractTrueTable
    {
        public SimplifiedTruthTableCreator(IParser parser) : base(parser)
        {
        }

        private void GetSuccessFailList(out ICollection<bool[]> success, out ICollection<bool[]> fail,
            ref bool[][] data)
        {
            success = new List<bool[]>();
            fail = new List<bool[]>();
            foreach (var items in data)
            {
                var clone = (bool[]) items.Clone();
                var outcome = GetResults(ref clone);
                if (outcome != null)
                    if (outcome.Value)
                        success.Add(items);
                    else
                        fail.Add(items);
            }
        }

        private byte[][] TransformArray(bool[][] data)
        {
            var result = new byte[data.Length][];
            for (var i = 0; i < data.Length; i++)
            {
                result[i] = new byte[data[i].Length];
                for (var j = 0; j < data[i].Length; j++)
                    result[i][j] = (byte) (data[i][j] ? 1 : 0);
            }
            return result;
        }

        private byte[][] SimplfyList(byte[][] data)
        {
            ICollection<byte[]> newData = new List<byte[]>();
            var clonedData = (byte[][]) data.Clone();

            foreach (byte[] current in data)
            {
                var foundMatch = false;
                foreach (byte[] check in data)
                {
                    if (check.Equals(current)) continue;

                    var differ = ArrayUtils.GetDifferIndexes(current, check);

                    if (differ.Length == 1)
                    {
                        foundMatch = true;
                        var temp = (byte[]) check.Clone();
                        temp[differ[0]] = 2;
                        if (!ArrayUtils.ContainsArrayInList(temp, ref newData))
                            newData.Add(temp);
                    }
                }
                
                if (!foundMatch && newData.Any())
                {
                    newData.Add(current);
                }
            }

            if (newData.Count == 0)
                return data;
            return SimplfyList(newData.ToArray());
        }


        public override byte[][] GetTable()
        {
            ICollection<bool[]> success;
            ICollection<bool[]> fail;

            var data = GetAllOptions(_operator.GetArguments().Length);

            GetSuccessFailList(out success, out fail, ref data);

            var result = new List<byte[]>();
            for (var i = 0; i < 2; i++)
            {
                var collection = i == 0 ? fail.ToArray() : success.ToArray();

                foreach (var items in SimplfyList(TransformArray(collection)))
                {
                    var array = new byte[items.Length + 1];
                    Array.Copy(items, array, items.Length);
                    array[items.Length] = (byte) i;
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