﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using RabbitMQ.Client.Events;

namespace RabbitMqTour
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsumerSimple1();
            ProducerSimple1();
            Console.ReadLine();
            MqConnectionFactory.Dispose();
        }

        public static void ProducerSimple1()
        {


            var conn = MqConnectionFactory.GetConn(new RabbitMqConfig());

            using (var channel = conn.CreateModel())
            {
                channel.QueueDeclare("TestQueue", true, false, false, null);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                for (int i = 0; i < 20; i++)
                {
                    string message = $"你好，我是第{i.ToString()}条信息";
                    channel.BasicPublish("", "TestQueue", null, message.ToBytes());
                    //Console.WriteLine($"信息发送---{i.ToString()}");
                }
            }
        }

        public static void ConsumerSimple1()
        {

            var conn = MqConnectionFactory.GetConn(new RabbitMqConfig());

            var channel = conn.CreateModel();
            channel.QueueDeclare("TestQueue", true, false, false, null);
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                Console.WriteLine($"接收到消息：{body.ToStr()}");
                if (new Random().Next(0, 12) == 1)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                    Console.WriteLine("!!!!!!!!!");
                }
                else
                {
                    channel.BasicNack(ea.DeliveryTag, false, true);
                    Console.WriteLine("确认失败!");
                }
            };
            channel.BasicConsume("TestQueue", false, consumer);
        }
    }
}