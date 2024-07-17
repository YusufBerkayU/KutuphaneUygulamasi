using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutuphaneUygulamasi.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PdfFilePath",
                table: "Books",
                newName: "CoverImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverImagePath",
                table: "Books",
                newName: "PdfFilePath");
        }
    }
}
