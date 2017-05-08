using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Consumer3();
            Console.ReadLine();
            MqConnectionFactory.Dispose();
        }

        static void Consumer3()
        {
            string[] routingkeys = new string[] { "black", "green", "red", "white", "blue" };

            var conn = MqConnectionFactory.GetConn(new RabbitMqConfig());
            var channel = conn.CreateModel();
            channel.ExchangeDeclare("Direct_log", ExchangeType.Direct, durable: true);
            var queueName = "ColorQueue";
            for (int i = 0; i < routingkeys.Length; i++)
            {
                if (i % 2 == 0)
                {
                    channel.QueueBind(queueName, "Direct_log", routingkeys[i]);
                }
            }

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var bodyMessage = ea.Body.ToStr();
                var routeKey = ea.RoutingKey;
                Console.WriteLine($"routeKey:{routeKey}");
                Console.WriteLine($"msg:{bodyMessage}");
            };
            channel.BasicConsume(queueName, true, consumer);
        }
    }
}
