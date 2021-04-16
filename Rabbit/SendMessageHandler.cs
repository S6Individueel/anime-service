/*using AnimeService.Data.Models;
using AnimeService.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
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
    public class SendMessageHandler : IHostedService, IDisposable
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly IConnection _connection;
        private readonly IAnimeRepository repository;

        private readonly ILogger<SendMessageHandler> _logger;
        private Timer _timer;
        private int executionCount = 0;

        private readonly HttpClient _httpclient;

        public SendMessageHandler(IAnimeRepository repository, ILogger<SendMessageHandler> logger, HttpClient httpClient)
        {
            _logger = logger;
            connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            _connection = connectionFactory.CreateConnection();
            this.repository = repository;
            _httpclient = httpClient;
        }

        public async void send(*//*IList<TopAnime> shows*//*)
        {
            using (_connection)
            using (var channel = _connection.CreateModel())
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
                Console.WriteLine(" [x] Sent '{0}':'{1}'", "shows.trending", body);
            }
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);
            send();
            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
*/