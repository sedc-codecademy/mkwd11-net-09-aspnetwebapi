using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.NoteScaffoldedApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name:"Address",
                table:"Users",
                type:"nvarchar(50)",
                nullable:true
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users"
                );
        }
    }
}
