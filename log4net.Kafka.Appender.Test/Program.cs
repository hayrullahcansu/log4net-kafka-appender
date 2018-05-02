using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net.Config;

namespace log4net.Kafka.Appender.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            GlobalContext.Properties["component"] = "send";


            logger.Debug("this Debug msg " + DateTime.Now.ToShortTimeString());
            //topic pattern  :   testapp-%property{component}-%class-%method-%level
            //the log's topic:   testapp-send-log4net.Kafka.Appender.Test.Program-Main-DEBUG

            //message pattern:   %d [%t] %-5p %c %m%n 
            //message        :   2018-05-02 17:49:05,033 [1] DEBUG log4net.Kafka.Appender.Test.Program this Debug msg 5:49 PM

        }
    }
}
