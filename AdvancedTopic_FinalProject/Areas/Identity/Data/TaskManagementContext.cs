using AdvancedTopic_FinalProject.Areas.Identity.Data;
using AdvancedTopic_FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Task = AdvancedTopic_FinalProject.Models.Task;

namespace AdvancedTopic_FinalProject.Data;

public class TaskManagementContext : IdentityDbContext<MainUser>
{
    public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
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

    public DbSet<Project> Projects { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Task> Tasks { get; set; }

    public ICollection<RoleTask> RoleTasks { get; set; } = new List<RoleTask>();

    public ICollection<RoleProject> RoleProjects { get; set; } = new List<RoleProject>();
}
