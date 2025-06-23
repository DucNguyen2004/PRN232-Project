using System.Text.Json.Serialization;

namespace BusinessObjects;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
