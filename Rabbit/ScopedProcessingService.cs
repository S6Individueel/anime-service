using AnimeService.Data.Models;
using AnimeService.Rabbit.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnimeService.Rabbit
{
    internal class ScopedProcessingService : IScopedProcessingService
    {
        private int executionCount = 0;
        private readonly double minutesTillUpdate = 720.0; 
        private readonly ILogger _logger;

/*        private readonly ConnectionFactory connectionFactory;
        private readonly IConnection _connection;*/
        private readonly HttpClient _httpclient;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger, HttpClient httpClient)
        {
/*            connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            _connection = connectionFactory.CreateConnection();*/
            _httpclient = httpClient;

            _logger = logger;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation(
                    "Scoped Processing Service is working. Count: {Count}", executionCount);

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "topic_logs",                     //EXCHANGE creation
                                            type: "topic");

                    string uri = "https://api.jikan.moe/v3" + "/top/anime/0/airing";
                    HttpResponseMessage response = await _httpclient.GetAsync(uri);
                    var content = await response.Content.ReadAsStringAsync();

                    IList<JToken> results = JObject.Parse(content)["top"].Children().ToList(); //Parses content, gets the "top" list and converts to list.

                    IList<TopAnime> topAnimes = new List<TopAnime>();
                    foreach (JToken anime in results)
                    {
                        TopAnime topAnime = anime.ToObject<TopAnime>();
                        topAnimes.Add(topAnime);
                    }

                    var json = JsonConvert.SerializeObject(topAnimes);                    //MESSAGE creation
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "topic_logs",                        //MESSAGE publishing
                                         routingKey: "shows.trending",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent Update message", body);
                }
                await Task.Delay(TimeSpan.FromMinutes(minutesTillUpdate), stoppingToken);
            }
        }
    }
}
