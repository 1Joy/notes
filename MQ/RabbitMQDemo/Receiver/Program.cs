using RabbitMQ.Client;
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
                HostName = "192.168.99.100",
                Port=5672,
                VirtualHost = "my_vhost",
                UserName = "admin",
                Password = "admin"
            };
            //获取连接对象
            var connection = connectionFactory.CreateConnection();
            //获取连接的管道
            IModel channel = connection.CreateModel();

            //P2PModel(channel);
            //WorkQueueAutoAck(channel);
            //WorkQueueAck(channel);

            //FanoutModel(channel);
            //RoutingDirectModel(channel);
            RoutingTopicModel(channel);

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


        /// <summary>
        /// 广播模型(发布/订阅模型) 扇形交换机
        /// </summary>
        /// <param name="channel"></param>
        public static void FanoutModel(IModel channel)
        {
            //绑定交换机
            channel.ExchangeDeclare("logs", "fanout");

            //创建临时队列，获取队列名称
            //使用生成的名称创建一个非持久的，排他的，自动删除的队列，降低的服务压力
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
            channel.QueueBind(queueName, "logs_direct", "info");
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

        /// <summary>
        /// 路由模型之订阅模型(动态路由)
        /// </summary>
        /// <param name="channel"></param>
        public static void RoutingTopicModel(IModel channel)
        {
            //省略绑定交换机步骤
            channel.ExchangeDeclare("logs_topic", "topic");
            //创建临时队列
            string queueName = channel.QueueDeclare().QueueName;

            //绑定队列到交换机,此时需要指定绑定的路由
            channel.QueueBind(queueName, "logs_topic", "*.info");

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
