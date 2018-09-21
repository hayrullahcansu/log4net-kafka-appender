# log4net-kafka-appender
logger-log4net appender for kafka which provides the custom topics pattern and partitions

## Supported frameworks 
```
netstandart2.0+
net4.5+
dotnet core 1.0+
```

## Getting Started
### Step 1: Install log4net.Kafka.Appender package from [nuget.org](https://www.nuget.org/packages/log4net.Kafka.Appender/)
```
Install via Package-Manager   Install-Package log4net.Kafka.Appender
Install via .NET CLI          dotnet add package log4net.Kafka.Appender
```
### Step 2: Configure log4net sections

```xml
<?xml version="1.0" encoding="utf-8" ?>
<log4net>
   <appender name="KafkaAppender" type="log4net.Kafka.Appender.KafkaAppender, log4net.Kafka.Appender">
      <KafkaSettings>
        <brokers>
          <add value="127.0.0.1:9092" />
        </brokers>
        <topic type="log4net.Layout.PatternLayout">
          <!--<conversionPattern value="kafka.logstash.%level" />-->
          <conversionPattern value="appname-%property{component}-%class-%method-%level" />
        </topic>
      </KafkaSettings>
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%d [%t] %-5p %c %m%n" />
      </layout>
    </appender>
  <root>
    <level value="DEBUG"/>
    <appender-ref ref="KafkaAppender" />
  </root>
</log4net>
```
## Usage

```cs
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
```
