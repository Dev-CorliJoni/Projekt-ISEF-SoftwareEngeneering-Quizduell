using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quixduell.ServiceLayer.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddFKUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Studysets_StudysetId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Studysets_StudysetId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Studysets_AspNetUsers_CreatorId",
                table: "Studysets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStudysetConnection_AspNetUsers_UserId",
                table: "UserStudysetConnection");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudysetId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudysetId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudysetId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudysetId1",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "StudysetContributors",
                columns: table => new
                {
                    ContributedStudysetsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContributorsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudysetContributors", x => new { x.ContributedStudysetsId, x.ContributorsId });
                    table.ForeignKey(
                        name: "FK_StudysetContributors_AspNetUsers_ContributorsId",
                        column: x => x.ContributorsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudysetContributors_Studysets_ContributedStudysetsId",
                        column: x => x.ContributedStudysetsId,
                        principalTable: "Studysets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsersRequestedToBecomeContributor",
                columns: table => new
                {
                    UsersRequestedToBecomeContributorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersRequestedToBecomeContributorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRequestedToBecomeContributor", x => new { x.UsersRequestedToBecomeContributorId, x.UsersRequestedToBecomeContributorsId });
                    table.ForeignKey(
                        name: "FK_UsersRequestedToBecomeContributor_AspNetUsers_UsersRequestedToBecomeContributorId",
                        column: x => x.UsersRequestedToBecomeContributorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersRequestedToBecomeContributor_Studysets_UsersRequestedToBecomeContributorsId",
                        column: x => x.UsersRequestedToBecomeContributorsId,
                        principalTable: "Studysets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudysetContributors_ContributorsId",
                table: "StudysetContributors",
                column: "ContributorsId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRequestedToBecomeContributor_UsersRequestedToBecomeContributorsId",
                table: "UsersRequestedToBecomeContributor",
                column: "UsersRequestedToBecomeContributorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Studysets_AspNetUsers_CreatorId",
                table: "Studysets",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStudysetConnection_AspNetUsers_UserId",
                table: "UserStudysetConnection",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studysets_AspNetUsers_CreatorId",
                table: "Studysets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStudysetConnection_AspNetUsers_UserId",
                table: "UserStudysetConnection");

            migrationBuilder.DropTable(
                name: "StudysetContributors");

            migrationBuilder.DropTable(
                name: "UsersRequestedToBecomeContributor");

            migrationBuilder.AddColumn<Guid>(
                name: "StudysetId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudysetId1",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudysetId",
                table: "AspNetUsers",
                column: "StudysetId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudysetId1",
                table: "AspNetUsers",
                column: "StudysetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Studysets_StudysetId",
                table: "AspNetUsers",
                column: "StudysetId",
                principalTable: "Studysets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Studysets_StudysetId1",
                table: "AspNetUsers",
                column: "StudysetId1",
                principalTable: "Studysets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Studysets_AspNetUsers_CreatorId",
                table: "Studysets",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStudysetConnection_AspNetUsers_UserId",
                table: "UserStudysetConnection",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
