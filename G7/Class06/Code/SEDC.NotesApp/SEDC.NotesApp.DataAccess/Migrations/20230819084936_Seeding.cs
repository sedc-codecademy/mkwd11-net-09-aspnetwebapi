using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.NotesApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "FirstName", "LastName", "UserName" },
                values: new object[] { 1, 27, "Dragan", "Manaskov", "dmanaskov" });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Priority", "Tag", "Text", "Title", "UserId" },
                values: new object[] { 1, 3, 1, "go to work", "Work", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
