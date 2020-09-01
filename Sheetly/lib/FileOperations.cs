using System;
using System.Collections.Generic;

namespace Sheetly.lib
{
    class FileOperations
    {
        public static List<List<string>> Unique(List<List<string>> list_1, int list_1Index)
        {
            List<List<string>> finalList = new List<List<string>>();
            List<string> processedValues = new List<string>();
            
            foreach (List<string> row in list_1)
            {
                if (processedValues.Contains(row[list_1Index]))
                    continue;

                finalList.Add(row);
                processedValues.Add(row[list_1Index]);
            }

            return finalList;
        }

        public static int CountCommon(List<List<string>> list_1, int list_1Index, List<List<string>> list_2, int list_2Index)
        {
            int commonCount = 0;

            List<string> list_1Values = new List<String>();
            foreach (List<string> row in list_1)
            {
                list_1Values.Add(row[list_1Index]);
            }


            foreach (List<string> row in list_2)
            {
                if (list_1Values.Contains(row[list_2Index]))
                    commonCount++;
            }

            return commonCount;
        }
    }
}
