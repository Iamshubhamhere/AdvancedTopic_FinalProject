using AdvancedTopic_FinalProject.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace AdvancedTopic_FinalProject.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Project title is required.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Project title must be between 5 and 200 characters.")]
        public string title { get; set; }


        public string MainUserID { get; set; }
        public MainUser MainUser { get; set; }




        
        public HashSet<Task> Tasksids { get; set; } = new HashSet<Task>();


        public ICollection<RoleProject> RoleProjects { get; set; } = new List<RoleProject>();



    }
}
