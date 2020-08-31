using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Authentication.Migrations
{
    public partial class usernameChangedToIdentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                schema: "Authentication",
                table: "TokenAuthenticationDetails");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationId",
                schema: "Authentication",
                table: "TokenAuthenticationDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificationId",
                schema: "Authentication",
                table: "TokenAuthenticationDetails");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                schema: "Authentication",
                table: "TokenAuthenticationDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
