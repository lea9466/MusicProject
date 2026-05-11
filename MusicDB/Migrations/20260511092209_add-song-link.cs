using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicDB.Migrations
{
    /// <inheritdoc />
    public partial class addsonglink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SongLink",
                table: "SongRequests",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongLink",
                table: "SongRequests");
        }
    }
}
