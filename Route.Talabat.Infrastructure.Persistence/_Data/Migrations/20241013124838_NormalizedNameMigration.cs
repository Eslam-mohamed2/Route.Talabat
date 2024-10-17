using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route.Talaat.Infrastructure.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class NormalizedNameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "products");
        }
    }
}
