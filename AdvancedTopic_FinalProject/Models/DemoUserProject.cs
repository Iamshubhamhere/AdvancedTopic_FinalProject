using AdvancedTopicsAuthDemo.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AdvancedTopicsAuthDemo.Models
{
    public class DemoUserProject
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public DemoUser DemoUser { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
