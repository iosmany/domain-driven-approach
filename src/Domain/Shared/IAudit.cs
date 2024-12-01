
namespace App.Domain.Shared;

public interface IAudit 
{
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
}

public class Audit : Entity
{
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected Audit()
    {
    }

    public Audit(DateTime createdAt, DateTime updatedAt) : this()
    {
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}