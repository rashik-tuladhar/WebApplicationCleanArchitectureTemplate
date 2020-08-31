using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Authentication.Migrations
{
    public partial class fieldsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactNo",
                schema: "Authentication",
                table: "TokenAuthenticationDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Authentication",
                table: "TokenAuthenticationDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "Authentication",
                table: "TokenAuthenticationDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNo",
                schema: "Authentication",
                table: "TokenAuthenticationDetails");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Authentication",
                table: "TokenAuthenticationDetails");

            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "Authentication",
                table: "TokenAuthenticationDetails");
        }
    }
}
