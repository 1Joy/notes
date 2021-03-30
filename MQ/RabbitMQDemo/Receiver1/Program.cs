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
                //设置主机
                HostName = "192.168.99.100",
                //设置端口号
                Port = 5672,
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
            WorkQueueAck(channel);

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
    }
}
