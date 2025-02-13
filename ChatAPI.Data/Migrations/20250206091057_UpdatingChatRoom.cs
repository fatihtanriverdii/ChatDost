using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingChatRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "ChatRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "ChatRooms");
        }
    }
}
