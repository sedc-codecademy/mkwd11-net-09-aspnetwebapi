using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.NoteScaffoldedApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeNameAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Users",
                newName: "HomeAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HomeAddress",
                table: "Users",
                newName: "Address");
        }
    }
}
