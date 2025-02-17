using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class edittedChatRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ChatRooms",
                newName: "RoomName");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "ChatRooms",
                newName: "RoomCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomName",
                table: "ChatRooms",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "RoomCode",
                table: "ChatRooms",
                newName: "Code");
        }
    }
}
