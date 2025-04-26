using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBookIsbnSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISPN",
                table: "Book",
                newName: "ISBN");

            migrationBuilder.RenameIndex(
                name: "IX_Book_ISPN",
                table: "Book",
                newName: "IX_Book_ISBN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "Book",
                newName: "ISPN");

            migrationBuilder.RenameIndex(
                name: "IX_Book_ISBN",
                table: "Book",
                newName: "IX_Book_ISPN");
        }
    }
}
