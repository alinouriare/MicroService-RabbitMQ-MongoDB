using Common.Entities;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace EventsSender
{
    class Program
    {
        public static async Task Main()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc => {
                var host = sbc.Host(new Uri("rabbitmq://127.0.0.1"), h => {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            await bus.StartAsync();
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
                await bus.Publish(peyment);
                Console.WriteLine("".PadLeft(100, '-'));
                Console.Write("Do you want to continue (y/n)? ");
                string answer = Console.ReadLine();
                continuePayment = answer.ToLower() == "y";
            } while (continuePayment);


            await bus.StopAsync();

        }
    }
}
