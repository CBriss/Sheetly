
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sheetly.lib
{
    class FileArithmetic
    {
        public static List<List<string>> Add(List<List<string>> list_1, List<List<string>> list_2)
        {
            List<List<string>> finalList = new List<List<string>>();

            foreach(List<string> row in list_1)
            {
                finalList.Add(row);
            }

            foreach (List<string> row in list_2)
            {
                finalList.Add(row);
            }

            return finalList;
        }

        public static List<List<string>> Subtract(List<List<string>> list_1, int list_1Index, List<List<string>> list_2, int list_2Index)
        {
            List<List<string>> finalList = new List<List<string>> ();

            List<string> list_2Values = new List<String>();
            foreach (List<string> row in list_2)
            {
                list_2Values.Add(row[list_2Index]);
            }

            foreach (List<string> row in list_1)
            {
                if (list_2Values.Contains(row[list_1Index]))
                    continue;
                
                finalList.Add(row);
            }

            return finalList;
        }

        public static List<List<string>> Combine(List<List<string>> list_1, int list_1Index, List<List<string>> list_2, int list_2Index)
        {
            List<List<string>> finalList = new List<List<string>>();

            // Add Headers to final list
            finalList.Add((List<string>)list_1[0].Concat(list_2[0]));

            Dictionary<string, List<string>> list_2Values = new Dictionary<string, List<string>>();
            foreach (List<string> row in list_2)
            {
                list_2Values[row[list_2Index]] = row;
            }

            foreach (List<string> row in list_1)
            {
                if (list_2Values.ContainsKey(row[list_1Index]))
                {
                    finalList.Add((List<string>)row.Concat(list_2Values[row[list_1Index]]));
                }
            }

            return finalList;
        }        
    }
}
