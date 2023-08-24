using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.NoteApp.DataAccess.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "FirstName", "LastName", "Username" },
                values: new object[] { 1, 34, "Viktor", "Jakovlev", "vjakovlev" });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Priority", "Tag", "Text", "UserId" },
                values: new object[] { 1, 1, 3, "note text", 1 });
        }

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
