﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace Receiver
{
    class Program
    {
        //消费者和生产者需要连接到同一个虚拟主机，同一个用户，同一个通道，队列才能接受相应的消息
        static void Main(string[] args)
        {
            //创建连接工厂对象
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                //设置主机
                HostName = "192.168.99.100",
                //设置端口号
                Port=5672,
                //可设置虚拟主机
                VirtualHost = "my_vhost",
                //设置用户名(默认是guest)
                UserName = "admin",
                //设置密码(默认是guest)
                Password = "admin"
            };
            //获取连接对象
            var connection = connectionFactory.CreateConnection();
            //获取连接的管道
            IModel channel = connection.CreateModel();

            //P2PModel(channel);
            //WorkQueueAutoAck(channel);
            WorkQueueAck(channel);

            //在消费者这一端，不建议关闭连接，因为监听消息是一个异步的，如果关闭了连接可能会造成监听不了消息
            Console.Read();
        }

        /// <summary>
        /// 直连消息模型
        /// </summary>
        public static void P2PModel(IModel channel)
        {
            //管道绑定队列
            channel.QueueDeclare("helloQueue", false, false, false, null);
            //创建消费者
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, e) =>
            {
                Console.WriteLine($"消费消息:{Encoding.UTF8.GetString(e.Body.ToArray())}");
            };

            //消费消息
            /* queue：队列名称
             * autoAck：自动确认消费消息，不需要手动确认
             * consumer：消费者
             */
            channel.BasicConsume("helloQueue", true, consumer);
        }

        /// <summary>
        /// 工作队列消息模型——自动确认消息(轮询)
        /// 消息确认机制：
        ///     autoAck：消息自动确认机制
        ///         true：消费者自动向rabbitmq确认消息消费，不关心消息是否被执行完；如果有的消费者执行期间宕机，就会发生消息丢失（因为消息确认后就会从队列中移除）channel.BasicConsume("helloQueue", true, consumer);
        ///         false：消费者不会确认消息，即使消费了消息，不经过手动确认，都不会把消息从队列中移除；可以让消费者一次接受n个消息，手动确认n个消息
        /// </summary>
        /// <param name="channel"></param>
        public static void WorkQueueAutoAck(IModel channel)
        {
            //通道绑定队列
            channel.QueueDeclare("workQueue1", false, false, false, null);

            //创建消费者
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, e) =>
            {
                Console.WriteLine($"消费消息:{Encoding.UTF8.GetString(e.Body.ToArray())}");
            };

            channel.BasicConsume("workQueue1", true, consumer);
        }

        /// <summary>
        /// 工作队列消息模型——手动确认消息
        /// </summary>
        /// <param name="channel"></param>
        public static void WorkQueueAck(IModel channel)
        {
            //通道绑定队列
            channel.QueueDeclare("workQueue1", false, false, false, null);
            //让通道一次只接受一条消息
            channel.BasicQos(0, 1, false);

            //创建消费者
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, e) =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"消费消息:{Encoding.UTF8.GetString(e.Body.ToArray())}");
                //手动确认消息
                /* DeliveryTag：消息标识
                 * multiple：是否确认多条消息
                 */
                channel.BasicAck(e.DeliveryTag, false);
            };

            channel.BasicConsume("workQueue1", false, consumer);
        }
    }
}
