namespace PersonAPI.Models;

public class PersonModel
{
    public Guid Id { get; init; }
    public string Name { get; private set; } = string.Empty;
}
