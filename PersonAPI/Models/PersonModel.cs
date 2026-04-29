namespace PersonAPI.Models;

public class PersonModel
{
    public PersonModel(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Guid Id { get; init; }
    public string Name { get; private set; } = string.Empty;
}
