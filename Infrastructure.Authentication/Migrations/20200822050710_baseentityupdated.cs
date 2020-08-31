using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Authentication.Migrations
{
    public partial class baseentityupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Authentication",
                table: "TokenAuthenticationDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "Authentication",
                table: "TokenAuthenticationDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Authentication",
                table: "TokenAuthenticationDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "Authentication",
                table: "TokenAuthenticationDetails");
        }
    }
}
