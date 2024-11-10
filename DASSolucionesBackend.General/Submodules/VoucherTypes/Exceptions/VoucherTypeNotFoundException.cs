namespace DASSolucionesBackend.General.Submodules.VoucherTypes.Exceptions;

public class VoucherTypeNotFoundException : NotFoundException
{
    public VoucherTypeNotFoundException(Guid id) 
        : base("VoucherType", id)
    {
    }
}