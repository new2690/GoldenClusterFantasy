using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.Messaging;

/// <summary>
/// Service for publishing messages to RabbitMQ.
/// سرویس برای انتشار پیام‌ها به RabbitMQ.
/// </summary>
public class RabbitMQService : IMessageQueueService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    /// <summary>
    /// Initializes a new instance of the RabbitMQService.
    /// سازنده‌ای برای ایجاد نمونه جدید از RabbitMQService.
    /// </summary>
    /// <param name="connectionString">RabbitMQ connection string / رشته اتصال RabbitMQ.</param>
    public RabbitMQService(string connectionString)
    {
        // Create a connection factory
        // ایجاد کارخانه اتصال
        var factory = new ConnectionFactory { Uri = new Uri(connectionString) };

        // Establish connection and channel
        // برقراری اتصال و کانال
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    /// <summary>
    /// Publishes a message to the specified queue.
    /// انتشار یک پیام به صف مشخص‌شده.
    /// </summary>
    /// <param name="queueName">Name of the queue / نام صف.</param>
    /// <param name="message">Message to publish / پیام برای انتشار.</param>
    public void PublishMessage(string queueName, string message)
    {
        // Declare the queue if it doesn't exist
        // اعلام صف اگر وجود نداشته باشد
        _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        // Convert message to bytes
        // تبدیل پیام به بایت
        var body = Encoding.UTF8.GetBytes(message);

        // Publish the message to the queue
        // انتشار پیام به صف
        _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    }

    /// <summary>
    /// Disposes the connection and channel.
    /// آزادسازی اتصال و کانال.
    /// </summary>
    public void Dispose()
    {
        // Close the channel and connection
        // بستن کانال و اتصال
        _channel?.Close();
        _connection?.Close();
    }
}