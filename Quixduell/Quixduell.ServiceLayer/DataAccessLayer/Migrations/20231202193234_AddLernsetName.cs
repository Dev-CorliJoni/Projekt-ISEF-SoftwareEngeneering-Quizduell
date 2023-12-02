using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quixduell.ServiceLayer.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddLernsetName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Lernsets_LernsetID",
                table: "Question");

            migrationBuilder.AlterColumn<Guid>(
                name: "LernsetID",
                table: "Question",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Lernsets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Lernsets_LernsetID",
                table: "Question",
                column: "LernsetID",
                principalTable: "Lernsets",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Lernsets_LernsetID",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Lernsets");

            migrationBuilder.AlterColumn<Guid>(
                name: "LernsetID",
                table: "Question",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Lernsets_LernsetID",
                table: "Question",
                column: "LernsetID",
                principalTable: "Lernsets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
