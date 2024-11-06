namespace DASSolucionesBackend.General.Submodules.Address.Countries.Entities;

public class Country : Entity<Guid>
{
    public string Name { get; private set; }
    public string? Code { get; private set; }
    public string? Demonym { get; private set; }
    public bool Status { get; private set; }
    
    private Country(Guid id, string name, string? code, string? demonym, bool status)
    {
        Id = id;
        Name = name;
        Code = code;
        Demonym = demonym;
        Status = status;
    }
    
    public static Country Create(string name, string? code, string? demonym)
    {
        Guid id = Guid.NewGuid();
        const bool status = true;
        return new Country(id, name, code, demonym, status);
    }
    
    public void Update(string name, string? code, string? demonym)
    {
        Name = name;
        Code = code;
        Demonym = demonym;
    }
    
    public void ChangeStatus(bool status)
    {
        Status = status;
    }
}