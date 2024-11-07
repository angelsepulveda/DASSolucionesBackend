using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Regions.Entities;

public class Region : Entity<Guid>
{
    public string Name { get; private set; }
    public Guid CountryId { get; private set; }
    public string? Code { get; private set; }
    public bool Status { get; private set; }
    
    public virtual Country Country { get; set; }
    
    private Region(Guid id, string name, Guid countryId, string? code, bool status)
    {
        Id = id;
        Name = name;
        CountryId = countryId;
        Code = code;
        Status = status;
    }
    
    public static Region Create(string name, Guid countryId, string? code)
    {
        Guid id = Guid.NewGuid();
        const bool status = true;
        return new Region(id, name, countryId, code, status);
    }
    
    public void Update(string name, Guid countryId,string? code)
    {
        Name = name;
        Code = code;
        CountryId = countryId;
    }
    
    public void ChangeStatus(bool status)
    {
        Status = status;
    }
}