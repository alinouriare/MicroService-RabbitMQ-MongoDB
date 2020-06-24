using Common.Entities;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace EventsReciever01
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
                sbc.ReceiveEndpoint("test_queue", ep => {
                    ep.Handler<Payment>(context=> {

                        return Console.Out.WriteLineAsync($"Payment Done. {context.Message.CardNumber} : { context.Message.AmountToPay} : {context.Message.Name}");

                    });
                
                });
            });
            await bus.StartAsync();
            Console.WriteLine("Press any key to continue ...");
            await Task.Run(() => Console.ReadKey());
            await bus.StopAsync();



        }
    }
}
