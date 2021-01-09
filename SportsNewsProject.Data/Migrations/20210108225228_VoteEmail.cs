using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsNewsProject.Migrations
{
    public partial class VoteEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "VoteEmail",
                table: "Comments",
                type: "text[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteEmail",
                table: "Comments");
        }
    }
}
