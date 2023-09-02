using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.NotesApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PasswordProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "FirstName", "LastName", "Username" },
                values: new object[] { 1, 27, "Dragan", "Manaskov", "dmanaskov" });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Priority", "Tag", "Text", "UserId" },
                values: new object[] { 1, 3, 1, "Go to work", 1 });
        }
    }
}
