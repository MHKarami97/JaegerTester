using System;
using System.Threading;

namespace JaegerTester
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Start");

                Console.WriteLine("Please Enter jaegerAgentHost ex:(172.20.19.68 or st-dt-tools) : ");
                var jaegerAgentHost = Console.ReadLine();

                Console.WriteLine("Please Enter jaegerAgentPort ex:(31392) : ");
                var jaegerAgentPort = Console.ReadLine();

                TraceLogger.InitOpenTelemetry("JaegerTester", jaegerAgentHost, Convert.ToInt32(jaegerAgentPort));

                Console.WriteLine("-----------------------------");
                Console.WriteLine("Start First Trace");
                using (new TraceLogger("TestAction"))
                {
                    Console.WriteLine("Wait 500 MilliSecond");
                    Thread.Sleep(500);
                    Console.WriteLine("FinishWait");
                    Console.WriteLine("----------");
                    Console.WriteLine("----------");
                }
                Console.WriteLine("End First Trace");
                Console.WriteLine("-----------------------------");
                
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Start Second Trace");
                using (new TraceLogger("TestAction"))
                {
                    Console.WriteLine("Wait 300 MilliSecond");
                    Thread.Sleep(300);
                    Console.WriteLine("FinishWait");
                    Console.WriteLine("----------");
                    Console.WriteLine("----------");
                }
                Console.WriteLine("End Second Trace");
                Console.WriteLine("-----------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}