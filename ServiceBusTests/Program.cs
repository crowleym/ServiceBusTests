using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Messages;
using Rhino.ServiceBus.LoadBalancer;

namespace ServiceBusTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            var container = builder.Build();

            new RhinoServiceBusConfiguration()
            .UseAutofac(container)
            .Configure();
            new LoadBalancerConfiguration()
            .UseAutofac(container)
            .Configure();

            var loadBalancer = container.Resolve<MsmqLoadBalancer>();
            loadBalancer.Start();

            var bus = container.Resolve<IStartableServiceBus>();
            bus.Start();

            Console.WriteLine("q to quit, anoy other key to send message");
            while (Console.ReadKey().KeyChar != 'q')
            {
                bus.Send(loadBalancer.Endpoint, new TestMessage() { Content = "Hello World!" + Guid.NewGuid() });

            }

            loadBalancer.Dispose();
            bus.Dispose();
            container.Dispose();
            Console.WriteLine();
        }
    }
}
