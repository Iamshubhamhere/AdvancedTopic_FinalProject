using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedTopic_FinalProject.Models
{
    public class RoleProject
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
