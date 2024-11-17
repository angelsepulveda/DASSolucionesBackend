namespace DASSolucionesBackend.Warehouses.Submodules.Categories.Exceptions;

public class CategoryAlreadyExistsException : Exception
{
    public CategoryAlreadyExistsException(string name)
        : base($"Category with name {name} already exists.")
    {
        
    }
}