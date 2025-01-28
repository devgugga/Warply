using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warply.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShortedUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortedUrl",
                table: "Urls",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortedUrl",
                table: "Urls");
        }
    }
}
