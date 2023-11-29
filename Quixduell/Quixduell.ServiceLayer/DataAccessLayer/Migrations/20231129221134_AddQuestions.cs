using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quixduell.ServiceLayer.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lernset_AspNetUsers_CreatorId",
                table: "Lernset");

            migrationBuilder.DropForeignKey(
                name: "FK_Lernset_Category_CategoryID",
                table: "Lernset");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Lernset_LernsetID",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Contributors_LernsetPermissions_Lernset_LernsetPermissionsID",
                table: "Relations_Contributors_LernsetPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lernset",
                table: "Lernset");

            migrationBuilder.RenameTable(
                name: "Lernset",
                newName: "Lernsets");

            migrationBuilder.RenameIndex(
                name: "IX_Lernset_CreatorId",
                table: "Lernsets",
                newName: "IX_Lernsets_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Lernset_CategoryID",
                table: "Lernsets",
                newName: "IX_Lernsets_CategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lernsets",
                table: "Lernsets",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lernsets_AspNetUsers_CreatorId",
                table: "Lernsets",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lernsets_Category_CategoryID",
                table: "Lernsets",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Lernsets_LernsetID",
                table: "Question",
                column: "LernsetID",
                principalTable: "Lernsets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_Contributors_LernsetPermissions_Lernsets_LernsetPermissionsID",
                table: "Relations_Contributors_LernsetPermissions",
                column: "LernsetPermissionsID",
                principalTable: "Lernsets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lernsets_AspNetUsers_CreatorId",
                table: "Lernsets");

            migrationBuilder.DropForeignKey(
                name: "FK_Lernsets_Category_CategoryID",
                table: "Lernsets");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Lernsets_LernsetID",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Contributors_LernsetPermissions_Lernsets_LernsetPermissionsID",
                table: "Relations_Contributors_LernsetPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lernsets",
                table: "Lernsets");

            migrationBuilder.RenameTable(
                name: "Lernsets",
                newName: "Lernset");

            migrationBuilder.RenameIndex(
                name: "IX_Lernsets_CreatorId",
                table: "Lernset",
                newName: "IX_Lernset_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Lernsets_CategoryID",
                table: "Lernset",
                newName: "IX_Lernset_CategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lernset",
                table: "Lernset",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lernset_AspNetUsers_CreatorId",
                table: "Lernset",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lernset_Category_CategoryID",
                table: "Lernset",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Lernset_LernsetID",
                table: "Question",
                column: "LernsetID",
                principalTable: "Lernset",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_Contributors_LernsetPermissions_Lernset_LernsetPermissionsID",
                table: "Relations_Contributors_LernsetPermissions",
                column: "LernsetPermissionsID",
                principalTable: "Lernset",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
