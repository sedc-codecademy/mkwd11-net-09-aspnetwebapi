using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesAppScaffoldedG5.DataAccess.Migrations
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
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //        FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Users__3214EC077222C240", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Notes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
            //        Tag = table.Column<int>(type: "int", nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Notes__3214EC074CA8C1B0", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Notes__UserId__267ABA7A",
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
