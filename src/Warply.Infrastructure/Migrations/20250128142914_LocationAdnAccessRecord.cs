using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warply.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LocationAdnAccessRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LinkAccess",
                table: "Urls",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "UsersLocations",
                table: "Urls",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkAccess",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "UsersLocations",
                table: "Urls");
        }
    }
}
