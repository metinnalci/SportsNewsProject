using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SportsNewsProject.Migrations
{
    public partial class AdminUserCreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminMenu",
                table: "AdminMenu");

            migrationBuilder.RenameTable(
                name: "AdminMenu",
                newName: "AdminMenus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminMenus",
                table: "AdminMenus",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    SurName = table.Column<string>(type: "text", nullable: true),
                    EMail = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AddDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminMenus",
                table: "AdminMenus");

            migrationBuilder.RenameTable(
                name: "AdminMenus",
                newName: "AdminMenu");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminMenu",
                table: "AdminMenu",
                column: "ID");
        }
    }
}
