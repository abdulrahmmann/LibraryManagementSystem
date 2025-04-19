using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRequest_BookCopies_BookCopyId",
                table: "BorrowRequest");

            migrationBuilder.RenameColumn(
                name: "BookCopyId",
                table: "BorrowRequest",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowRequest_BookCopyId",
                table: "BorrowRequest",
                newName: "IX_BorrowRequest_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRequest_Book_BookId",
                table: "BorrowRequest",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRequest_Book_BookId",
                table: "BorrowRequest");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BorrowRequest",
                newName: "BookCopyId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowRequest_BookId",
                table: "BorrowRequest",
                newName: "IX_BorrowRequest_BookCopyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRequest_BookCopies_BookCopyId",
                table: "BorrowRequest",
                column: "BookCopyId",
                principalTable: "BookCopies",
                principalColumn: "CopyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
