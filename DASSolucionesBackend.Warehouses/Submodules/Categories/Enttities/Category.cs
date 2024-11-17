namespace DASSolucionesBackend.Warehouses.Submodules.Categories.Enttities;

public class Category : Entity<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool Status { get; private set; }
    
    private Category(Guid id, string name, string? description, bool status)
    {
        Id = id;
        Name = name;
        Description = description;
        Status = status;
    }
    
    public static Category Create(string name, string? description)
    {
        Guid id = Guid.NewGuid();
        const bool status = true;
        return new Category(id, name, description, status);
    }
     
    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }
    
    public void ChangeStatus(bool status)
    {
        Status = status;
    }
}