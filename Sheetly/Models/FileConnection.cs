using Sheetly.lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sheetly.Models
{
    public class FileConnection
    {
        #region Properties

        private File destinationFile;
        public File DestinationFile
        {
            get { return destinationFile; }
            set { destinationFile = value; }
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
