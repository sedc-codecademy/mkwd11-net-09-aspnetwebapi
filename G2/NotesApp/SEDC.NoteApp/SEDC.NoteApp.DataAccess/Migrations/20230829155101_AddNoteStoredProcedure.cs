using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.NoteApp.DataAccess.Migrations
{
    public partial class AddNoteStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.SP_AddNote
                    @Text nvarchar(100),
                    @Priority int,
                    @Tag int,
                    @UserId int
                AS
                BEGIN
                    INSERT INTO Notes (Text, Priority, Tag, UserId)
                    VALUES (@Text, @Priority, @Tag, @UserId);
                END;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.SP_AddNote");
        }
    }
}
