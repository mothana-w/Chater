using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chater.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeRoomsMembersKeyComposit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomsMembers",
                table: "RoomsMembers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoomsMembers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomsMembers",
                table: "RoomsMembers",
                columns: new[] { "RoomId", "MemeberId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomsMembers",
                table: "RoomsMembers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RoomsMembers",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomsMembers",
                table: "RoomsMembers",
                column: "Id");
        }
    }
}
