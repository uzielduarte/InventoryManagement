using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserTableMigrationCountryCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "País",
                table: "AspNetUsers",
                newName: "Country");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "AspNetUsers",
                newName: "País");
        }
    }
}
