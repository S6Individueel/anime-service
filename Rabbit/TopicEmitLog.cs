/*using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShowService
{
    class TopicEmitLog
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs",                     //EXCHANGE creation
                                        type: "topic");

                var routingKey = "shows.trending";
                var message = "Insert trending shows call here.";                   //MESSAGE creation
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "topic_logs",                        //MESSAGE publishing
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
            }
        }
    }
}
*/