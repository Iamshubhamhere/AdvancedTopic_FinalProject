using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvancedTopic_FinalProject.Migrations
{
    public partial class AddedModelProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CompletedTask",
                table: "Taasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedTask",
                table: "Taasks");
        }
    }
}
