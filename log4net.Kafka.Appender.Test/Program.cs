using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            logger.Debug("this Debug msg");
            logger.Warn("this Warn msg");
            logger.Info("this Info msg");
            logger.Error("this Error msg");
            logger.Fatal("this Fatal msg");

            try
            {
                var i = 0;
                var j = 5 / i;
            }
            catch (Exception ex)
            {
                logger.Error("this Error msg,中文测试", ex);
            }
            Console.WriteLine("OK");
            System.Console.ReadKey();
        }
    }
}
