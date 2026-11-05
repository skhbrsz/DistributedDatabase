using Confluent.Kafka;
using MongoDB.Driver;
using System.Text.Json;

var kafkaConfig = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "furniture-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest,
    EnableAutoCommit = false
};

var mongoClient = new MongoClient("mongodb://localhost:27017");

var database = mongoClient.GetDatabase("FurnitureStore");

var collection = database.GetCollection<Furniture>("Furniture");

using var consumer = new ConsumerBuilder<Ignore, string>(kafkaConfig).Build();

consumer.Subscribe("furniture");

Console.WriteLine("Consumer started. Waiting for furniture messages...");

while (true)
{
    try
    {
        var result = consumer.Consume();

        var furniture = JsonSerializer.Deserialize<Furniture>(
            result.Message.Value
        );

        if (furniture != null)
        {
            await collection.ReplaceOneAsync(
                x => x.Id == furniture.Id,
                furniture,
                new ReplaceOptions { IsUpsert = true }
            );

            consumer.Commit(result);

            Console.WriteLine($"Saved furniture: {furniture.Name}");
        }
    }
    catch (ConsumeException ex)
    {
        Console.WriteLine($"Kafka error: {ex.Error.Reason}");
    }
}