using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoLayer.Migrations
{
    public partial class CollabTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    LabelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<string>(nullable: true),
                    Userid = table.Column<long>(nullable: false),
                    Noteid = table.Column<long>(nullable: false),
                    notesNoteId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_Labels_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Labels_NotesTable_notesNoteId",
                        column: x => x.notesNoteId,
                        principalTable: "NotesTable",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Labels_Userid",
                table: "Labels",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_notesNoteId",
                table: "Labels",
                column: "notesNoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Labels");
        }
    }
}
