using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warply.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UrlStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UrlStatus",
                table: "Urls",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlStatus",
                table: "Urls");
        }
    }
}
