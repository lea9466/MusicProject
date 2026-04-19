using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicDB.Migrations
{
    /// <inheritdoc />
    public partial class songtips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tips",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tips",
                table: "Songs");
        }
    }
}
