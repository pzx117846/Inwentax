using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Inwentax.Models;

namespace Inwentax.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Inwentax.Models.Laptop> Laptops { get; set; } = default!;
        public DbSet<Inwentax.Models.Phone> Phone { get; set; } = default!;
        public DbSet<Inwentax.Models.Assignment> Assignments { get; set; } = default!;
        public DbSet<Inwentax.Models.UserViewModel> UserViewModel { get; set; } = default!;
        public DbSet<Inwentax.Models.Ticket> Tickets { get; set; } = default;
    }
}
