using AdvancedTopicsAuthDemo.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AdvancedTopicsAuthDemo.Models
{
    public class DemoUserTask
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        public DemoUser DemoUser { get; set; }

        public int TaaskId { get; set; }

        public Taask Taask { get; set; }
    }
}
