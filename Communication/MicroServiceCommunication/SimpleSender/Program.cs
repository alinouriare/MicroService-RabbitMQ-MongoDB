using Common.Entities;
using RabbitMQ.Client;
using System;
using Common.Tools;

namespace SimpleSender
{
    class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;
        private const string QueueName = "Simple_Queue";

        public static void Main()
        {
            CreateQueue();
            bool continuePayment;
            do
            {
                var peyment = new Payment();
                Console.Write("Please Enter Card Number:");
                peyment.CardNumber = Console.ReadLine();
                Console.Write("Please Enter Reciever Name:");
                peyment.Name = Console.ReadLine();
                Console.Write("Please Enter Amount To Pay:");
                peyment.AmountToPay = int.Parse(Console.ReadLine());
                SendMessage(peyment);
                Console.WriteLine("".PadLeft(100, '-'));
                Console.Write("Do you want to continue (y/n)? ");
                string answer = Console.ReadLine();
                continuePayment = answer.ToLower() == "y";
            } while (continuePayment);
            Console.ReadLine();
        }

        public static void CreateQueue()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.QueueDeclare(QueueName, true, false, false, null);
        }
        private static void SendMessage(Payment message)
        {
            _model.BasicPublish("", QueueName, null, message.ToByteArray());
            Console.WriteLine("Payment Message Sent : {0} : {1} : {2}", message.CardNumber, message.AmountToPay, message.Name);
        }
    }
}
