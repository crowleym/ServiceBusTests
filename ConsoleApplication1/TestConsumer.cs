using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.ServiceBus;
using Messages;

namespace ConsoleApplication1
{
    public class TestConsumer : ConsumerOf<TestMessage>
    {
        #region ConsumerOf<TestMessage> Members

        public void Consume(TestMessage message)
        {
            Console.WriteLine(message.Content);
            System.Threading.Thread.Sleep(2000); // simulate work
        }

        #endregion
    }
}
