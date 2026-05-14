using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Msa3edcomAdmin.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<SiteContent>      SiteContents      => Set<SiteContent>();
    public DbSet<SiteSettings>     SiteSettings      => Set<SiteSettings>();
    public DbSet<ClientItem>       ClientItems       => Set<ClientItem>();
    public DbSet<MediaItem>        MediaItems        => Set<MediaItem>();
    public DbSet<ServiceItem>      ServiceItems      => Set<ServiceItem>();
    public DbSet<PortfolioItem>    PortfolioItems    => Set<PortfolioItem>();
    public DbSet<TestimonialItem>  Testimonials      => Set<TestimonialItem>();
    public DbSet<FaqItem>          FaqItems          => Set<FaqItem>();
    public DbSet<TechStackItem>    TechStackItems    => Set<TechStackItem>();
    public DbSet<StatItem>         StatItems         => Set<StatItem>();
    public DbSet<ProcessStep>      ProcessSteps      => Set<ProcessStep>();
    public DbSet<LeadRequest>      LeadRequests      => Set<LeadRequest>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ClientItem>().HasIndex(x => x.DisplayOrder);
        builder.Entity<MediaItem>().HasIndex(x => x.Category);
        builder.Entity<MediaItem>().HasIndex(x => x.CreatedAt);
        builder.Entity<ServiceItem>().HasIndex(x => x.DisplayOrder);
        builder.Entity<PortfolioItem>().HasIndex(x => x.DisplayOrder);
        builder.Entity<TestimonialItem>().HasIndex(x => x.DisplayOrder);
        builder.Entity<FaqItem>().HasIndex(x => x.DisplayOrder);
        builder.Entity<TechStackItem>().HasIndex(x => x.DisplayOrder);
        builder.Entity<StatItem>().HasIndex(x => x.DisplayOrder);
        builder.Entity<ProcessStep>().HasIndex(x => x.DisplayOrder);
        builder.Entity<LeadRequest>().HasIndex(x => x.CreatedAt);
        builder.Entity<LeadRequest>().HasIndex(x => x.Status);
    }
}
