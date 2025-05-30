namespace Infrastructure.Messaging;

/// <summary>
/// Interface for message queue operations.
/// رابط برای عملیات صف پیام.
/// </summary>
public interface IMessageQueueService
{
    /// <summary>
    /// Publishes a message to the specified queue.
    /// انتشار یک پیام به صف مشخص‌شده.
    /// </summary>
    /// <param name="queueName">Name of the queue / نام صف.</param>
    /// <param name="message">Message to publish / پیام برای انتشار.</param>
    void PublishMessage(string queueName, string message);
}