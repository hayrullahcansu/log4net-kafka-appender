using Confluent.Kafka;
using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4net.Kafka.Appender
{
    public class KafkaAppender : AppenderSkeleton
    {
        private Producer producer;

        public KafkaSettings KafkaSettings { get; set; }


        public override void ActivateOptions()
        {
            base.ActivateOptions();
            Start();

        }
        private void Start()
        {
            try
            {
                var conf = new Dictionary<string, object>
                {
                  { "bootstrap.servers", KafkaSettings.Brokers.First() }
                };
                //TODO:  { "bootstrap.servers", KafkaSettings.Brokers.First() }
                // Apply Multiple brokers

                if (KafkaSettings == null) throw new LogException("KafkaSettings is missing");

                if (KafkaSettings.Brokers == null || KafkaSettings.Brokers.Count == 0) throw new Exception("Broker is not found");
                
                if (producer == null)
                {
                    var brokers = KafkaSettings.Brokers.Select(x => new Uri(x)).ToArray();
                    // var kafkaOptions = new KafkaOptions(brokers);
#if DEBUG
                    //kafkaOptions.Log = new ConsoleLog();
#else
					//kafkaOptions.Log = new KafkaLog();
#endif
                    producer = new Producer(conf);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Error("could not stop producer", ex);
            }

        }
        private void Stop()
        {

            try
            {
                producer?.Dispose();
            }
            catch (Exception ex)
            {
                ErrorHandler.Error("could not start producer", ex);
            }
        }
        private string GetTopic(LoggingEvent loggingEvent)
        {
            string topic = null;
            if (KafkaSettings.Topic != null)
            {
                var sb = new StringBuilder();
                using (var sw = new StringWriter(sb))
                {
                    KafkaSettings.Topic.Format(sw, loggingEvent);
                    topic = sw.ToString();
                }
            }

            if (string.IsNullOrEmpty(topic))
            {
                topic = $"{loggingEvent.LoggerName}.{loggingEvent.Level.Name}";
            }

            return topic;
        }
        private string GetMessage(LoggingEvent loggingEvent)
        {
            var sb = new StringBuilder();
            using (var sr = new StringWriter(sb))
            {
                Layout.Format(sr, loggingEvent);

                if (Layout.IgnoresException && loggingEvent.ExceptionObject != null)
                    sr.Write(loggingEvent.GetExceptionString());

                return sr.ToString();
            }
        }
        private int GetPartition(LoggingEvent loggingEvent)
        {
            int partition = 0;
            if (KafkaSettings.Partition != null)
            {
                var sb = new StringBuilder();
                using (var sw = new StringWriter(sb))
                {
                    KafkaSettings.Topic.Format(sw, loggingEvent);
                    var partitionPattern = sw.ToString();
                    partition = Utils.Utils.GetPartitionFromPattern(partitionPattern, 0);
                }
            }
            return partition;
        }
        protected override void Append(LoggingEvent loggingEvent)
        {
            var message = GetMessage(loggingEvent);
            var topic = GetTopic(loggingEvent);
            var partition = GetPartition(loggingEvent);
            Console.WriteLine($"Enabled Status {KafkaSettings.HashPartitionEnabled}");
            Console.WriteLine($"Max Partition {KafkaSettings.NumberMaxPartition}");
            Console.WriteLine($"Topic {topic}");
            Console.WriteLine($"Partition {partition}");
            var data = Encoding.UTF8.GetBytes(message);
            producer.ProduceAsync(topic, null, 0, 0, data, 0, data.Length, partition);
        }
        protected override void OnClose()
        {
            Console.WriteLine("7");

            base.OnClose();
            Stop();
        }
    }
}
