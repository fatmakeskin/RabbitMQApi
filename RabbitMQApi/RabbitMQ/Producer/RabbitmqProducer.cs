using RabbitMQApi.RabbitMQ.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQApi.RabbitMQ.Producer
{
    public class RabbitmqProducer
    {
        public void PublishData(string message,string queueName)
        {
            RabbitmqConnection rabbitmqConnection = new RabbitmqConnection();
            var body = Encoding.UTF8.GetBytes(message);
            rabbitmqConnection.GetChannel(queueName).BasicPublish("","ApiQueue",false,null,body);           

        }
    }
}
