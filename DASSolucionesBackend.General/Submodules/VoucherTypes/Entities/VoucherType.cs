namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Entities;

public class VoucherType : Entity<Guid>
{
    public string Name { get; private set; }
    public string? Code { get; private set; }
    public string? Description { get; private set; }
    public bool Status { get; private set; }
    
    private VoucherType(Guid id,string name, string? code, string? description, bool status)
    {
        Id = id;
        Name = name;
        Code = code;
        Description = description;
        Status = status;
    }
    
    public static VoucherType Create(string name, string? code, string? description)
    {
        Guid id = Guid.NewGuid();
        const bool status = true;
        return new VoucherType(id, name, code, description, status);
    }
    
    public void Update(string name, string? code, string? description)
    {
        Name = name;
        Code = code;
        Description = description;
    }
    
    public void ChangeStatus(bool status)
    {
        Status = status;
    }
}