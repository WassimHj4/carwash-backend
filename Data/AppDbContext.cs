using CarwashBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashBackend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
}
