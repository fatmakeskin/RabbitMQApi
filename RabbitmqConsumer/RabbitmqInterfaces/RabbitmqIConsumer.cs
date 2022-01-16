using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQApi.RabbitMQ.RabbitMQInterfaces
{
    interface RabbitmqIConsumer
    {
        void RabbitmqConsumer(string queue);
    }
}
