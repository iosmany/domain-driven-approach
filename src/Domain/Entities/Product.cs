

namespace App.Domain.Entities;


public class Product
{
    public string Name { get; private set; }
    public virtual ProductPrice Price { get; private set; }

    protected Product()
    {
    }

    public Product(string name, ProductPrice price) : this()
    {
        Name = name;
        Price = price;
    }
}