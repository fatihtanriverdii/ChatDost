using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingV2ChatRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "ChatRooms",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ChatRooms",
                newName: "userId");
        }
    }
}
