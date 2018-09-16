// <copyright file="RabbitMQService.cs" company="Majunga.co.uk">
// Copyright (c) Majunga.co.uk. All rights reserved.
// </copyright>

namespace MajungaLibrary.BusinessLogic.Services
{
    using System.Text;
    using MajungaLibrary.BusinessLogic.Services.Interfaces;
    using MajungaLibrary.BusinessLogic.Services.Models.MessageQueue;
    using Newtonsoft.Json;
    using RabbitMQ.Client;

    /// <summary>
    /// RabbitMQ Service
    /// <inheritdoc/>
    /// </summary>
    public class RabbitMQService : IMessageQueuing
    {
        private readonly IConnection rabbitMQConnection;
        private readonly IModel channel;
        private readonly MQConnection mqConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMQService"/> class.
        /// </summary>
        /// <param name="connection">MQ Connection Details</param>
        public RabbitMQService(MQConnection connection)
        {
            var factory = new ConnectionFactory() { HostName = connection.Host };
            this.rabbitMQConnection = factory.CreateConnection();
            this.channel = this.rabbitMQConnection.CreateModel();
            this.channel.QueueDeclare(
                queue: this.mqConnection.Channel,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            this.mqConnection = connection;
        }

        /// <inheritdoc/>
        public void QueueMessage(string serialisedMessage)
        {
            var body = Encoding.UTF8.GetBytes(serialisedMessage);

            this.channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: this.mqConnection.RoutingKey,
                    basicProperties: null,
                    body: body);
        }

        /// <inheritdoc/>
        public void QueueMessage<T>(T message)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            this.channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: this.mqConnection.RoutingKey,
                    basicProperties: null,
                    body: body);
        }

        /// <inheritdoc/>
        public string ReadMessageQueue()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public T ReadMessageQueue<T>()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public uint ReadQueueCount()
        {
            return this.channel.MessageCount(queue: this.mqConnection.Channel);
        }

        /// <summary>
        /// Dispose of RabbitMQ
        /// </summary>
        public void Dispose()
        {
            this.channel.Dispose();
            this.rabbitMQConnection.Dispose();
        }
    }
}
