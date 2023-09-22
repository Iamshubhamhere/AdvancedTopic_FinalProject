using AdvancedTopic_FinalProject.Models;
using Microsoft.AspNetCore.Identity;

namespace AdvancedTopic_FinalProject.Areas.Identity.Data;

// Add profile data for application users by adding properties to the MainUser class
public class MainUser : IdentityUser
{
    public HashSet<Project> Projects { get; set; } = new HashSet<Project>();
    
}

