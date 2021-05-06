using AnimeService.Data.Models;
using AnimeService.Rabbit.Interfaces;
using AnimeService.Repositories.Interfaces;
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
        private readonly double hoursTillUpdate = 12; 
        private readonly ILogger _logger;

/*        private readonly ConnectionFactory connectionFactory;
        private readonly IConnection _connection;*/
        private readonly HttpClient _httpclient;
        private IAnimeRepository _animeRepository;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger, HttpClient httpClient, IAnimeRepository animeRepository)
        {
/*            connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            _connection = connectionFactory.CreateConnection();*/
            _httpclient = httpClient;
            _animeRepository = animeRepository;
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
                    channel.ExchangeDeclare(exchange: "topic_exchange",                     //EXCHANGE creation
                                            type: "topic");

                    var showList = (await _animeRepository.GetTopTenAsync())            //Parses content, gets the "top" list and converts to list.
                            .Select(anime => anime.AsShowDTO());

                    var json = JsonConvert.SerializeObject(showList);                    //MESSAGE creation
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "topic_exchange",                        //MESSAGE publishing
                                         routingKey: "shows.anime.trending",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent Update message", body);
                }
                await Task.Delay(TimeSpan.FromHours(hoursTillUpdate), stoppingToken);
            }
        }
    }
}
