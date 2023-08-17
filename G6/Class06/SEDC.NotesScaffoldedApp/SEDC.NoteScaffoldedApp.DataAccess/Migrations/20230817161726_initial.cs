using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.NoteScaffoldedApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false),
            //        Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Users__3214EC0707EB0E70", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Notes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false),
            //        Text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Priority = table.Column<int>(type: "int", nullable: false),
            //        Tag = table.Column<int>(type: "int", nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Notes__3214EC071B383E17", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Notes__UserId__398D8EEE",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Notes_UserId",
            //    table: "Notes",
            //    column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
