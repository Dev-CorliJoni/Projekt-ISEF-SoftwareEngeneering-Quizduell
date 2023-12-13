using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quixduell.ServiceLayer.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddIsStoredToUserStudysetConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStored",
                table: "UserStudysetConnection",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStored",
                table: "UserStudysetConnection");
        }
    }
}
