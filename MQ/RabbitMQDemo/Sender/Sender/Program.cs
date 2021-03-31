using RabbitMQ.Client;
using System;
using System.Text;

namespace Sender
{
    class Program
    {
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
                Password="admin"
            };
            //获取连接对象
            var connection = connectionFactory.CreateConnection();
            //获取连接的管道
            IModel channel = connection.CreateModel();

            //P2PModel(channel);
            //WorkQueueAutoAck(channel);
            //FanoutModel(channel);
            //RoutingDirectModel(channel);
            RoutingTopicModel(channel);

            channel.Close();
            connection.Close();
            Console.Read();
        }

        /// <summary>
        /// 直连消息模型
        /// </summary>
        public static void P2PModel(IModel channel)
        {
            //让通道绑定队列
            /* queue：队列名称
             * durable：队列是否需要持久化；不持久化时重启服务队列消失
             * exclusive：是否独占队列；当前队列只允许当前连接使用
             * autoDelete：是否在消费完后自动删除队列；没有消费者连接，没有消息时自动删除队列
             * arguments：其他参数
             */
            channel.QueueDeclare("helloQueue", false, false, false, null);

            //发送消息
            /* exchange：交换机名称
             * routingKey：队列名称
             * basicProperties：传递消息额外设置
             * body：消息体
             */
            channel.BasicPublish("", "helloQueue", null, Encoding.UTF8.GetBytes("my first Rabbitmq Demo"));
        }


        /// <summary>
        /// 工作队列消息模型——自动确认消息(轮询)
        /// 这是工作队列默认的工作方式，rabbitmq将按顺序将消息发给下一个消费者，每个消费者分到的消息都是一样的
        /// 一次性将消息数目分给每个消费者
        /// </summary>
        /// <param name="channel"></param>
        public static void WorkQueueAutoAck(IModel channel)
        {
            //通道绑定队列
            channel.QueueDeclare("workQueue1", false, false, false, null);

            for(int i = 0; i < 20; i++)
            {
                channel.BasicPublish("", "workQueue1", null, Encoding.UTF8.GetBytes($"{i} my first workQueue1 demo"));
            }
        }

        /// <summary>
        /// 广播模型(发布/订阅模型) 扇形交换机
        /// 1：有多个消费者，每个消费者都有自己的队列
        /// 2：每个队列都绑定到交换机上面
        /// 3：生产者只能发送消息到交换机，交换机来决定把消息发送到绑定的队列
        /// 4：队列的消费者都能拿到消息，实现一条消息被多个消费者消费
        /// </summary>
        /// <param name="channel"></param>
        public static void FanoutModel(IModel channel)
        {
            //绑定交换机
            /* exchange：交换机名称，没有就会被创建
             * type：交换机类型；fanout：扇形交换机
             */
            channel.ExchangeDeclare("logs", "fanout");

            //发送消息
            //此时发送消息只需要指定交换机的名称，不需要指定路由，因为此时交换机的类型是广播类型，路由无效
            channel.BasicPublish("logs", "", null, Encoding.UTF8.GetBytes("fanout type message"));
        }


        /// <summary>
        /// 路由模型之订阅模型(直连)
        /// </summary>
        /// <param name="channel"></param>
        public static void RoutingDirectModel(IModel channel)
        {
            //声明交换机，直连交换机
            channel.ExchangeDeclare("logs_direct", "direct");

            string routingKey = "info";

            //发送消息
            //此时会指定路由
            channel.BasicPublish("logs_direct", routingKey, null, Encoding.UTF8.GetBytes($"direct {routingKey} message"));
        }

        /// <summary>
        /// 路由模型之订阅模型(动态路由)
        /// </summary>
        /// <param name="channel"></param>
        public static void RoutingTopicModel(IModel channel)
        {
            //声明交换机，直连交换机
            channel.ExchangeDeclare("logs_topic", "topic");

            string routingKey = "user.info";

            //发送消息
            //此时会指定路由
            channel.BasicPublish("logs_topic", routingKey, null, Encoding.UTF8.GetBytes($"topic {routingKey} message"));
        }
    }
}
