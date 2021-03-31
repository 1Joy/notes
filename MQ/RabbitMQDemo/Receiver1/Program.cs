using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace Receiver1
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建连接工厂对象
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = "192.168.99.100",
                Port = 5672,
                VirtualHost = "my_vhost",
                UserName = "admin",
                Password = "admin"
            };
            //获取连接对象
            var connection = connectionFactory.CreateConnection();
            //获取连接的管道
            IModel channel = connection.CreateModel();

            //WorkQueueAck(channel);
            //FanoutModel(channel);
            RoutingDirectModel(channel);

            //在消费者这一端，不建议关闭连接，因为监听消息是一个异步的，如果关闭了连接可能会造成监听不了消息
            Console.Read();
        }

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
                Console.WriteLine($"消费消息(快):{Encoding.UTF8.GetString(e.Body.ToArray())}");
                //手动确认消息
                /* DeliveryTag：消息标识
                 * multiple：是否确认多条消息
                 */
                channel.BasicAck(e.DeliveryTag, false);
            };

            channel.BasicConsume("workQueue1", false, consumer);
        }

        /// <summary>
        /// 广播模型(发布/订阅模型) 扇形交换机
        /// </summary>
        /// <param name="channel"></param>
        public static void FanoutModel(IModel channel)
        {
            //绑定交换机
            channel.ExchangeDeclare("logs", "fanout");

            //创建临时队列，获取队列名称
            //有消息时就会被创建，消费完消息就会删除队列，降低的服务压力
            string queueName = channel.QueueDeclare().QueueName;

            //绑定队列到交换机
            //因为此时是广播类型交换机所以路由无效
            channel.QueueBind(queueName, "logs", "");

            //消费消息
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, e) =>
            {
                Console.WriteLine($"消费消息:{Encoding.UTF8.GetString(e.Body.ToArray())}");
            };

            //自动确认消息
            channel.BasicConsume(queueName, true, consumer);
        }

        /// <summary>
        /// 路由模型之订阅模型(直连)
        /// </summary>
        /// <param name="channel"></param>
        public static void RoutingDirectModel(IModel channel)
        {
            //省略绑定交换机步骤
            channel.ExchangeDeclare("logs_direct", "direct");
            //创建临时队列
            string queueName = channel.QueueDeclare().QueueName;

            //绑定队列到交换机,此时需要指定绑定的路由
            channel.QueueBind(queueName, "logs_direct", "error");

            //消费消息
            //消费消息
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, e) =>
            {
                Console.WriteLine($"消费消息{e.RoutingKey}:{Encoding.UTF8.GetString(e.Body.ToArray())}");
            };

            //自动确认消息
            channel.BasicConsume(queueName, true, consumer);
        }
    }
}
