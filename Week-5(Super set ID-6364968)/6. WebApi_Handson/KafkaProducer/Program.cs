using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static readonly CancellationTokenSource cts = new CancellationTokenSource();

    static async Task Main(string[] args)
    {
        Console.CancelKeyPress += (_, e) => {
            e.Cancel = true;
            cts.Cancel();
            Console.WriteLine("\nShutting down...");
        };

        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            // Recommended reliability settings
            EnableDeliveryReports = true,
            Acks = Acks.All,
            MessageTimeoutMs = 5000,
            SocketTimeoutMs = 30000,
            MessageSendMaxRetries = 3,
            RetryBackoffMs = 1000
        };

        try
        {
            using var producer = new ProducerBuilder<Null, string>(config)
                .SetLogHandler((_, logMessage) =>
                {
                    Console.WriteLine($"[Kafka] {logMessage.Level}: {logMessage.Message}");
                })
                .SetErrorHandler((_, error) =>
                {
                    Console.WriteLine($"[Kafka Error] {error.Reason}");
                    if (error.IsFatal) cts.Cancel();
                })
                .Build();

            Console.WriteLine("Kafka Chat Producer Started");
            Console.WriteLine("Type messages (press Ctrl+C or type 'quit' to exit):");

            while (!cts.IsCancellationRequested)
            {
                Console.Write("You: ");
                var message = await ReadLineAsync(cts.Token);

                if (string.IsNullOrWhiteSpace(message)) continue;
                if (message.Equals("quit", StringComparison.OrdinalIgnoreCase)) break;

                var messageWithTimestamp = $"[{DateTime.Now:HH:mm:ss}] {message}";

                try
                {
                    var deliveryResult = await producer.ProduceAsync(
                        "chat-messages",
                        new Message<Null, string> { Value = messageWithTimestamp },
                        cts.Token
                    );

                    Console.WriteLine($"Message delivered to partition {deliveryResult.Partition}, offset {deliveryResult.Offset}");
                }
                catch (ProduceException<Null, string> ex)
                {
                    Console.WriteLine($"Failed to deliver message: {ex.Error.Reason}");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Message delivery cancelled");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
        }
        finally
        {
            cts.Dispose();
            Console.WriteLine("Producer shut down completed");
        }
    }

    private static async Task<string> ReadLineAsync(CancellationToken cancellationToken)
    {
        string input = null;
        var task = Task.Run(() => {
            input = Console.ReadLine();
        }, cancellationToken);

        try
        {
            await task;
            return input;
        }
        catch (TaskCanceledException)
        {
            return string.Empty;
        }
    }
}
