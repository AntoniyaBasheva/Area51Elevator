namespace Area51Elevator
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Program
    {
        static void Main()
        {
            var @base = new Base();
            var agentThreads = new List<Thread>();

            for (int i = 1; i < 4; i++)
            {
                var agent = new Agent(i.ToString(), @base);
                var thread = new Thread(agent.EnterBase);
                thread.Start();
                agentThreads.Add(thread);
            }

            foreach (var agent in agentThreads) agent.Join();

            Console.ReadLine();
        }
    }
}
