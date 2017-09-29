using System.Collections.Generic;
using System.Linq;

namespace Logic.Utils
{
    public class ArrayUtils
    {
        public static int[] GetDifferIndexes<T>(T[] array1, T[] array2)
        {
            var indexes = new List<int>();
            for (var i = 0; i < array1.Length; i++)
                if (!array1[i].Equals(array2[i]))
                    indexes.Add(i);
            return indexes.ToArray();
        }

        public static bool ContainsArrayInList<T>(T[] item, ref ICollection<T[]> collection)
        {
            foreach (var check in collection)
            {
                var found = true;
                for (var i = 0; i < item.Length; i++)
                    if (!item[i].Equals(check.ElementAt(i)))
                    {
                        found = false;
                        break;
                    }
                if (found)
                    return true;
            }
            return false;
        }
    }
}