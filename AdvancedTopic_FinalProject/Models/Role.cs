using AdvancedTopic_FinalProject.Areas.Identity.Data;

namespace AdvancedTopic_FinalProject.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

        public string MainRoleID { get; set; }
        public MainRole MainRole { get; set; }

        public ICollection<RoleProject> RoleProjects { get; set; } = new List<RoleProject>();

        public ICollection<RoleTask> RoleTasks { get; set; } = new List<RoleTask>();
    }
}
