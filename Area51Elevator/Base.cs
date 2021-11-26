namespace Area51Elevator
{
    using System.Collections.Generic;
    using System.Threading;

    public class Base
    {
        private readonly List<Agent> agents = new();
        private readonly Semaphore semaphore = new(10, 10);

        public void Enter(Agent agent)
        {
            semaphore.WaitOne();

            lock(agents)
            {
                agents.Add(agent);
            }
        }

        public void Leave(Agent agent)
        {
            lock (agents)
            {
                agents.Remove(agent);
            }

            semaphore.Release();
        }
    }
}
