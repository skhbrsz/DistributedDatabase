public class Furniture
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Material { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public DateTime UpdatedAt { get; set; }
}