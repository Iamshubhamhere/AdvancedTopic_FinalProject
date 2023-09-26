using AdvancedTopicsAuthDemo.Areas.Identity.Data;
using AdvancedTopicsAuthDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AdvancedTopicsAuthDemo.Data;

public class ATAuthDemoContext : IdentityDbContext<DemoUser>
{
    public ATAuthDemoContext(DbContextOptions<ATAuthDemoContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Project> Projects { get; set; } = default!;
    public DbSet<Taask> Taasks { get; set; } = default!;
    public DbSet<DemoUserProject> DemoUserProjects { get; set; } = default!;
    public DbSet<DemoUserTask> DemoUserTasks { get; set; } = default!;
}
