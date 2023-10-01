using Microsoft.EntityFrameworkCore;

namespace CustomAPI.Models;

public class CustomContext : DbContext
{
    public CustomContext(DbContextOptions<CustomContext> options)
        : base(options)
    {
    }

    public DbSet<CustomItem> CustomItems { get; set; } = null!;
}