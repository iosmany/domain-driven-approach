
namespace App.Domain.Shared;

public interface IAudit 
{
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
}