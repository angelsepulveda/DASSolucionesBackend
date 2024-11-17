namespace DASSolucionesBackend.Warehouses.Submodules.Categories.Exceptions;

public class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(Guid id) 
        : base("Category", id)
    {
    } 
}