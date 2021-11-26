namespace Area51Elevator
{
    using System;
    using System.Threading;

    public class Agent
    {
        enum SecurityLevel { Confidential, Secret, TopSecret };

        enum Floor { G, S, T1, T2 };

        enum BaseActivity { Walk, CallElevator, Leave }

        Random random = new Random();

        public Agent(string name, Base @base)
        {
            Name = name;
            Base = @base;
        }

        public string Name { get; set; }

        public Base Base { get; set; }

        public void EnterBase()
        {
            Console.WriteLine($"Agent {Name} arrived in the base.");
            Base.Enter(this);
            Thread.Sleep(1000);

            var staysAtBase = true;
            while (staysAtBase)
            {
                var activity = GetRandomActivity();
                switch (activity)
                {
                    case BaseActivity.Walk:
                        WalkAround();
                        break;
                    case BaseActivity.CallElevator:
                        CallElevator();
                        break;
                    case BaseActivity.Leave:
                        Console.WriteLine($"Agent {Name} has left the base.");
                        Base.Leave(this);
                        staysAtBase = false;
                        break;
                    default: throw new NotImplementedException();
                }
            }
        }

        private void CallElevator()
        {
            var securityLevel = GetRandomSecurityLevel();
            var desiredFloor = GetRandomFloorRequest();

            Console.WriteLine($"Agent {Name} calls the elevator for floor: {desiredFloor}");

            if (securityLevel is SecurityLevel.Confidential && desiredFloor is not Floor.G
                || securityLevel is SecurityLevel.Secret && desiredFloor is Floor.T1 || desiredFloor is Floor.T2)
            {
                Console.WriteLine($"Agent {Name} has no permission to access {desiredFloor} floor.");
            }
            else
            {
                Console.WriteLine($"Agent {Name} authenticated successfully on {desiredFloor} floor.");
            }
        }

        private void WalkAround()
        {
            Console.WriteLine($"Agent {Name} is walking around in the base.");
            Thread.Sleep(1000);
        }

        private SecurityLevel GetRandomSecurityLevel()
        {
            var number = random.Next(10);
            if (number < 3) return SecurityLevel.TopSecret;
            if (number < 8) return SecurityLevel.Secret;
            return SecurityLevel.Confidential;
        }

        private Floor GetRandomFloorRequest()
        {
            var number = random.Next(10);
            if (number < 2) return Floor.S;
            if (number < 4) return Floor.G;
            if (number < 9) return Floor.T1;
            return Floor.T2;
        }

        private BaseActivity GetRandomActivity()
        {
            int n = random.Next(10);
            if (n < 3) return BaseActivity.Walk;
            if (n < 8) return BaseActivity.CallElevator;
            return BaseActivity.Leave;
        }
    }
}
