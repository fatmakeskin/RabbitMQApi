using RabbitMQ.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQApi.RabbitMQ.Connection
{
    public class RabbitmqConnection
    {
        public RabbitmqConnection()
        {
            GetConnection();
        }
        public IConnection Connection { get; set; }
        public bool IsConnected { get; set; } = false;

        private static readonly object _lockObj = new object();


        public IConnection GetConnection()
        {
            if (IsConnected)
                return Connection;
            try
            {
                lock (_lockObj)
                {
                    if (IsConnected)
                        return Connection;
                    ConnectionFactory connectionFactory = new ConnectionFactory
                    {
                        Uri = new Uri("amqp://guest:guest@localhost:5672")
                    };
                    Connection = connectionFactory.CreateConnection();
                    IsConnected = true;
                    return Connection;

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Rabbitmq baglantısı saglanamadı");
                return null;
            }
        }
        public IModel GetChannel(string queue)
        {
            GetConnection();
            IModel channel = Connection.CreateModel();
            channel.QueueDeclare(queue);
            return channel;
        }


    }

}
