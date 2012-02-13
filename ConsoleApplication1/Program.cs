using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus;
using Messages;
using Autofac;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TestConsumer>();

            var container = builder.Build();

            new RhinoServiceBusConfiguration()
            .UseAutofac(container)
            .Configure();

            var bus = container.Resolve<IStartableServiceBus>();
            bus.Start();

            Console.WriteLine("Waiting...");
            Console.ReadKey();

            bus.Dispose();
            container.Dispose();
        }
    }
}
