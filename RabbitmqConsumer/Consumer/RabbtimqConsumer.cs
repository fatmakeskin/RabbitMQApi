using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQApi.RabbitMQ.Connection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQApi.RabbitMQ.RabbitMQInterfaces;
using DataAccess.Repositoies;
using DataAccess.Model;
using DataAccess.UnitofWork;

namespace RabbitmqConsumer.Consumer
{
    public class RabbtimqConsumer : RabbitmqIConsumer
    {
        public RabbtimqConsumer()
        {
        }
        private IModel channel;

        private EventingBasicConsumer eventingBasicConsumer;

        private RabbitmqConnection rabbitMqServices;

        public RabbitmqConnection RabbitMqServices
        {
            get
            {
                if (rabbitMqServices == null)
                {
                    rabbitMqServices = new RabbitmqConnection();
                }
                return rabbitMqServices;

            }
            set => rabbitMqServices = value;
        }

        public void RabbitmqConsumer(string queue)
        {
            using (var connection = rabbitMqServices.GetConnection())
            {
                try
                {
                    if (string.IsNullOrEmpty(queue))
                    {
                        Log.Information("Consumer QueueName was null");
                    }
                    channel = RabbitMqServices.GetChannel(queue);
                    channel.BasicQos(0, 250, false);
                    eventingBasicConsumer = new EventingBasicConsumer(channel);
                    eventingBasicConsumer.Received += EventingBasicConsumerOnReceived;
                    channel.BasicConsume(queue, false, eventingBasicConsumer);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "RabbitMQ/RabbitmqConsumer");
                }

            }
        }
        private void EventingBasicConsumerOnReceived(object sender, BasicDeliverEventArgs e)
        {
            User model = JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(e.Body.ToArray()));
            try
            {
                using (var uow=new UnitofWork())
                {
                    var result=uow._masterContext.Add(model);                    
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Consumer kayıt edemedi!");
            }
            channel.BasicAck(e.DeliveryTag, true);
        }
    }
}
