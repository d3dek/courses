using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Utils
{
    public sealed class Config
    {
        public Config(int noOfDatabaseRetry)
        {
            NoOfDatabaseRetry = noOfDatabaseRetry;
        }

        public int NoOfDatabaseRetry { get; }
    }
}
