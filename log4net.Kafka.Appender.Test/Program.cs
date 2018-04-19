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
            while (true)
            {

                logger.Debug("this Debug msg " + DateTime.Now.ToShortTimeString());
                Thread.Sleep(1000);
                logger.Warn("this Warn msg" + DateTime.Now.ToShortTimeString());
                Thread.Sleep(1000);
                logger.Info("this Info msg" + DateTime.Now.ToShortTimeString());
                Thread.Sleep(1000);
                logger.Error("this Error msg" + DateTime.Now.ToShortTimeString());
                Thread.Sleep(1000);
                logger.Fatal("this Fatal msg" + DateTime.Now.ToShortTimeString());
                Thread.Sleep(7000);
            }
            Console.WriteLine("OK");
            System.Console.ReadKey();
        }
    }
}
