using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quixduell.ServiceLayer.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedRequestinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudysetId1",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudysetId1",
                table: "AspNetUsers",
                column: "StudysetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Studysets_StudysetId1",
                table: "AspNetUsers",
                column: "StudysetId1",
                principalTable: "Studysets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Studysets_StudysetId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudysetId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudysetId1",
                table: "AspNetUsers");
        }
    }
}
