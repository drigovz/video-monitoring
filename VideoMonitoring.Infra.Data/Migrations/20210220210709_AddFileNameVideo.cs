using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoMonitoring.Infra.Data.Migrations
{
    public partial class AddFileNameVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Video",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Video");
        }
    }
}
