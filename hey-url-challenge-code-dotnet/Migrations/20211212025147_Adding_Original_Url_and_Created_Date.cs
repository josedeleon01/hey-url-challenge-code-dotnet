using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hey_url_challenge_code_dotnet.Migrations
{
    public partial class Adding_Original_Url_and_Created_Date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Urls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OriginalUrl",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "OriginalUrl",
                table: "Urls");
        }
    }
}
