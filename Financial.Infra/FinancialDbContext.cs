using Financial.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Financial.Infra
{
    public class FinancialDbContext : DbContext
    {
        public virtual DbSet<Category> Category => Set<Category>();
        public virtual DbSet<SubCategory> SubCategory => Set<SubCategory>();

        public FinancialDbContext(DbContextOptions<FinancialDbContext> options) : base(options)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}
