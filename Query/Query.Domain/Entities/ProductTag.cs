namespace Query.Domain.Entities;

public class ProductTag
{
    public string Name { get; set; }

    public ProductTag(string name) => Name = name;
}
