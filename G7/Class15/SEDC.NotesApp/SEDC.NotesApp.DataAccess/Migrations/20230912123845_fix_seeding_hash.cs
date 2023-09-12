using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.NotesApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fix_seeding_hash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "21232F297A57A5A743894A0E4A801FC3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "!#/)zW??C?JJ??");
        }
    }
}
