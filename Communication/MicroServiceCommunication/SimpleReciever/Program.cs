using Common.Entities;
using Common.Tools;
using RabbitMQ.Client;

using System;

namespace SimpleReciever
{
    public class Program
    {
     
        private static IConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;
        private static QueueingBasicConsumer _consumer;
        private const string QueueName = "Simple_Queue";
        static void Main()
        {
            CreateQueue();
            Recieve();
        }

        public static void CreateQueue()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.QueueDeclare(QueueName, true, false, false, null);
            _consumer = new QueueingBasicConsumer(_model);
        }
        public static void Recieve()
        {
            var msgCount = GetMessageCount(_model, QueueName);
            var count = 0;
            _model.BasicConsume(QueueName, true, _consumer);
            while (count++ < msgCount)
            {
                var message = _consumer.Queue.Dequeue().Body.ToType<Payment>();
                Console.WriteLine($"Payment Done. {message.CardNumber} : { message.AmountToPay} : {message.Name}");
            }

        }

        private static uint GetMessageCount(IModel channel, string queueName)
        {
            var results = channel.QueueDeclare(queueName, true, false, false, null);
            return results.MessageCount;

        }
    }
}
