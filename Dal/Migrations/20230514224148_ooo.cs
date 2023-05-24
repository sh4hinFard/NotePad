using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    public partial class ooo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notebook_users_UserId",
                table: "Notebook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notebook",
                table: "Notebook");

            migrationBuilder.RenameTable(
                name: "Notebook",
                newName: "notebooks");

            migrationBuilder.RenameIndex(
                name: "IX_Notebook_UserId",
                table: "notebooks",
                newName: "IX_notebooks_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_notebooks",
                table: "notebooks",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_notebooks_users_UserId",
                table: "notebooks",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notebooks_users_UserId",
                table: "notebooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notebooks",
                table: "notebooks");

            migrationBuilder.RenameTable(
                name: "notebooks",
                newName: "Notebook");

            migrationBuilder.RenameIndex(
                name: "IX_notebooks_UserId",
                table: "Notebook",
                newName: "IX_Notebook_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notebook",
                table: "Notebook",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notebook_users_UserId",
                table: "Notebook",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
