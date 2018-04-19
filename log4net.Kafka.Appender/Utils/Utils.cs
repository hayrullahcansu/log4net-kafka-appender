using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4net.Kafka.Appender.Utils
{
    public static class Utils
    {
        public static int GetPartitionFromPattern(string pattern, int maxPartition = 100)
        {
            if (String.IsNullOrEmpty(pattern)) return 0;
            return maxPartition;
        }
    }
}
