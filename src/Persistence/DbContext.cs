


namespace App.Persistence;

using Microsoft.EntityFrameworkCore;

class ApplicationDbContext : DbContext
{

    

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

}