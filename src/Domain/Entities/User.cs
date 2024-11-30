


namespace App.Domain.Entities;

using App.Domain.Shared;

public class User: Entity, IAudit
{
    public virtual Name Name { get; private set; } 
    public Email Email { get; private set; } 

    public virtual Address InvoiceAddress { get; set; }
    public virtual Address? ShippingAddress { get; set; }

    public string Password { get; private set; } 
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected User()
    {
    }

    public User(Name name, Email email, string password) : this()
    {
        Name = name;
        Email = email;
        Password = password;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}