using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
