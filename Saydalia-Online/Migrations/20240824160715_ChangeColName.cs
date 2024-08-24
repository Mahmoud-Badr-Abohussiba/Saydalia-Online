using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saydalia_Online.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Medicines",
                newName: "ImageName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Medicines",
                newName: "Image");
        }
    }
}
