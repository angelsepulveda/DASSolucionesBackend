using DASSolucionesBackend.General.Submodules.Address.Countries.Entities;
using DASSolucionesBackend.General.Submodules.Address.Regions.Entities;

namespace DASSolucionesBackend.General.Submodules.Address.Localities.Entities;

public class Locality : Entity<Guid>
{
    public string Name { get; private set; }
    public Guid RegionId { get; private set; }
    public Guid CountryId { get; private set; }
    public string? Code { get; private set; }
    public bool Status { get; private set; }
    
    public virtual Region Region { get; set; }
    public virtual Country Country { get; set; }

    private Locality(Guid id, string name, Guid regionId, Guid countryId, string? code, bool status)
    {
        Id = id;
        Name = name;
        RegionId = regionId;
        CountryId = countryId;
        Code = code;
        Status = status;
    }

    public static Locality Create(string name, Guid regionId, Guid countryId, string? code)
    {
        Guid id = Guid.NewGuid();
        const bool status = true;
        return new Locality(id, name, regionId, countryId, code, status);
    }

    public void Update(string name,Guid regionId,Guid countryId, string? code)
    {
        Name = name;
        Code = code;
        RegionId = regionId;
        CountryId = countryId;
    }

    public void ChangeStatus(bool status)
    {
        Status = status;
    }
}