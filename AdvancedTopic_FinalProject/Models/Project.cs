using AdvancedTopicsAuthDemo.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace AdvancedTopicsAuthDemo.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Project title is required.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Project title must be between 5 and 200 characters.")]
        public string title { get; set; }


        public string DemoUserID { get; set; }
        public DemoUser DemoUser { get; set; }





        public HashSet<Taask> Tasksids { get; set; } = new HashSet<Taask>();


        //public ICollection<DemoUserProject> DemoUserProjects { get; set; } = new List<DemoUserProject>();



    }
}
