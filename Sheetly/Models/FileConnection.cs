using Sheetly.lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sheetly.Models
{
    public class FileConnection
    {
        #region Properties

        private SpreadsheetFile nextFile;
        public SpreadsheetFile NextFile
        {
            get { return nextFile; }
            set { nextFile = value; }
        }

        private AllowedOperations operation;
        public AllowedOperations Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        #endregion
    }
}
