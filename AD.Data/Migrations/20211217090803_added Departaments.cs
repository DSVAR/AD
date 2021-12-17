using Microsoft.EntityFrameworkCore.Migrations;

namespace AD.Data.Migrations
{
    public partial class addedDepartaments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Departaments",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Departaments",
                table: "AspNetUsers");
        }
    }
}
