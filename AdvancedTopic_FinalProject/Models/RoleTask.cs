using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedTopic_FinalProject.Models
{
    public class RoleTask
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int TaskId { get; set; }

        public Task Task { get; set; }
    }
}
