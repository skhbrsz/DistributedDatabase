using Confluent.Kafka;
using Microsoft.Data.SqlClient;
using System.Text.Json;

string connectionString =
    "Server=.\\SQLEXPRESS;Database=DistributedDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

string topic = "furniture";

var kafkaConfig = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

using var producer = new ProducerBuilder<string, string>(kafkaConfig).Build();

await using var connection = new SqlConnection(connectionString);

await connection.OpenAsync();

Console.WriteLine("Connected to SQL Server.");

string query = "SELECT Id, Name, Category, Material, Color, Price, UpdatedAt FROM Furniture";

using var command = new SqlCommand(query, connection);

await using var reader = await command.ExecuteReaderAsync();

while (await reader.ReadAsync())
{
    var furniture = new Furniture
    {
        Id = reader.GetInt32(0),
        Name = reader.GetString(1),
        Category = reader.GetString(2),
        Material = reader.GetString(3),
        Color = reader.GetString(4),
        Price = reader.GetDecimal(5),
        UpdatedAt = reader.GetDateTime(6)
    };

    string message = JsonSerializer.Serialize(furniture);

    var kafkaMessage = new Message<string, string>
    {
        Key = furniture.Id.ToString(),
        Value = message
    };

    await producer.ProduceAsync(topic, kafkaMessage);

    Console.WriteLine($"Sent furniture: {furniture.Name}, Price: {furniture.Price}, Color: {furniture.Color}, Material: {furniture.Material}");
}

Console.WriteLine("All furniture records sent to Kafka.");
